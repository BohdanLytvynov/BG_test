using AutoMapper;
using BookStore.BLL.Dto.UserDto;
using BookStore.BLL.Extensions;
using BookStore.BLL.MediatR.Account.RegisterCommands;
using BookStore.BLL.Services.CookieServices.Interfaces;
using BookStore.BLL.Services.TokenServices.Interfaces;
using BookStore.BLL.Services.TokenServices.Realizations;
using BookStore.DAL.Entities;
using BookStore.DAL.Enums;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Account.Register
{
    public class RegisteruserCommandHandler : IRequestHandler<RegisterUserCommand, Result<string>>
    {
        private readonly UserManager<User> _userManager;
        private readonly JWTTokenConfiguration _tokensConfiguration;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly ICookieService _cookieService;
        private readonly IHttpContextAccessor _contextAccessor;

        public RegisteruserCommandHandler(
            UserManager<User> userManager,
            JWTTokenConfiguration tokenConfiguration,
            IMapper mapper,
            ITokenService tokenService,
            ICookieService cookieService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
            _cookieService = cookieService;
            _contextAccessor = httpContextAccessor;
            _tokensConfiguration = tokenConfiguration;
        }

        public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _mapper.Map<User>(request.dto);

                //Generate Special Token Id
                var TokenId = Guid.NewGuid();

                //Check if username already exists
                var userExists = await _userManager.FindByNameAsync(request.dto.nickname);

                if (userExists is not null)
                    throw new Exception("Login is already in use!");

                user.AccessTokenIds.Add(new AccessTokenId() { AccessTokenGUID = TokenId, User = user });

                //Create User
                //var r = await _userManager.CreateAsync(user, request.dto.password);

                //if (!r.Succeeded)
                //{
                //    throw new Exception(r.GetErrors());
                //}
                //// Add User Role to User
                //r = await _userManager.AddToRoleAsync(user, UserRole.User.ToString());

                //if (!r.Succeeded)
                //{
                //    throw new Exception(r.GetErrors());
                //}

                //Generate JWT Access Token
                var tokenDto = await _tokenService.GenerateAccesToken(user, claims =>
                {
                    claims.Add(new Claim(JwtRegisteredClaimNames.Jti, TokenId.ToString()));
                });

                if (tokenDto is null)
                    throw new Exception("Fail to generate Access Token!");

                //Add Access Toke to Cookie

                await _cookieService.AppendCookiesToResponseAsync(
                    _contextAccessor!.HttpContext!.Response,
                    ("accessToken", tokenDto.AccessToken, new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddMinutes(_tokensConfiguration.AccessTokenExpirationMinutes),
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None
                    }));

                JObject resp = new JObject();
                resp["success"] = true;

                return Result.Ok(resp.ToString());

            }
            catch (Exception e)
            {
                return Result.Fail(new Error(e.Message));
            }
        }
    }
}
