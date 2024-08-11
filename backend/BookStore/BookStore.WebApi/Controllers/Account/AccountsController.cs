using BookStore.BLL.Dto.UserDto;
using BookStore.BLL.MediatR.Account.Register;
using BookStore.BLL.MediatR.Account.RegisterCommands;
using BookStore.WebApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers.Account
{    
    public class AccountsController : BaseApiController
    {
        IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            return HandleResult(await _mediator.Send(new RegisterUserCommand(dto)));
        }
    }
}
