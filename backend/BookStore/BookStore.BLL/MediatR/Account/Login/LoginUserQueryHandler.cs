using AutoMapper;
using BookStore.BLL.Dto.UserDto;
using BookStore.BLL.Exceptions.AccountExceptions;
using BookStore.BLL.Services.CookieServices.Interfaces;
using BookStore.BLL.Services.CookieServices.Realizations;
using BookStore.BLL.Services.TokenServices.Interfaces;
using BookStore.BLL.Services.TokenServices.Realizations;
using BookStore.DAL.Entities;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Account.Login
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, Result<AuthResponseDto>>
    {
        private readonly UserManager<User> _usermanager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICookieService _cookieService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly JWTTokenConfiguration _jwtTokenConfiguration;

        public LoginUserQueryHandler(UserManager<User> userManager,
            SignInManager<User> signInManager,
            JWTTokenConfiguration jwtTokenConfiguration,
            IHttpContextAccessor contextAccessor,
            ICookieService cookieService,
            ITokenService tokenService,
            IMapper mapper)
        {
            _usermanager = userManager;
            _signInManager = signInManager;
            _contextAccessor = contextAccessor;
            _tokenService = tokenService;
            _cookieService = cookieService;
            _jwtTokenConfiguration = jwtTokenConfiguration;
            _mapper = mapper;
        }

        public async Task<Result<AuthResponseDto>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _usermanager.FindByNameAsync(request.Dto.nickname);

                if (user is null)
                {
                    throw new IncorrectLoginOrPasswordException();
                }

                await _signInManager.SignOutAsync();

                var r = await _signInManager.CheckPasswordSignInAsync(user, request.Dto.password, false);

                if (!r.Succeeded)
                {
                    throw new IncorrectLoginOrPasswordException();
                }

                var newTokenGuid = Guid.NewGuid();

                var tokenExists = _contextAccessor.HttpContext.Request.Cookies.TryGetValue("accessToken", out var token);

                //Get Current Access Token
                if (!tokenExists)
                {
                    //There is no AccessToken for this user
                    user.AccessTokenIds.Add(new AccessTokenId() { User = user, AccessTokenGUID = newTokenGuid });
                }
                else
                {
                    //Update token in DB

                    //Get Current token from Cookie
                    var jti = _tokenService.GetUserClaimFromAccessToken(token, claimName: JwtRegisteredClaimNames.Jti);

                    if (string.IsNullOrEmpty(jti)) throw new Exception("Fail to get Data from accessToken!");
                    //Find old Token                                
                    var oldtoken = user.AccessTokenIds.FirstOrDefault(x => x.AccessTokenGUID.Equals(Guid.Parse(jti)));
                    //if there is already token in DB - update it
                    if (oldtoken is not null)
                    {
                        //Remove old Token
                        user.AccessTokenIds.Remove(oldtoken);
                        //Set new token
                        user.AccessTokenIds.Add(new AccessTokenId() { User = user, AccessTokenGUID = newTokenGuid });
                    }
                }
                              
                //Generate new JWT Access Token
                var tokenDto = await _tokenService.GenerateAccesToken(user, claims =>
                {
                    claims.Add(new Claim(JwtRegisteredClaimNames.Jti, newTokenGuid.ToString()));
                });

                if (tokenDto is null)
                    throw new Exception("Fail to generate Access Token!");

                //Add Access Toke to Cookie

                await _cookieService.AppendCookiesToResponseAsync(
                    _contextAccessor!.HttpContext!.Response,
                    ("accessToken", tokenDto.AccessToken, new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(1),
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        IsEssential = true,
                        Domain = $"localhost",                        
                        Path = "/"
                    }));
                
                await _usermanager.UpdateAsync(user);

                var responce = _mapper.Map<AuthResponseDto>(user);
                responce.status = true;
                return Result.Ok(responce);

            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
            
        }
    }
}
