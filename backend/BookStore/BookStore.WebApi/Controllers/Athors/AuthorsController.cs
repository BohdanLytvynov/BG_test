using BookStore.BLL.MediatR.Author.GetAll;
using BookStore.DAL.Entities;
using BookStore.WebApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers.Athors
{    
    public class AuthorsController : BaseApiController
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await _mediator.Send(new GetAllAuthorsQuery()));
        }
    }
}
