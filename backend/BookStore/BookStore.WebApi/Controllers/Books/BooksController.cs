using BookStore.BLL.Dto.Book;
using BookStore.BLL.MediatR.Books.Create;
using BookStore.BLL.MediatR.Books.GetAll;
using BookStore.BLL.MediatR.Books.GetById;
using BookStore.WebApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers.Books
{
    public class BooksController : BaseApiController
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await _mediator.Send(new GetAllBooksQuery()));
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            return HandleResult(await _mediator.Send(new GetBookByIdQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookDto dto)
        {
            return HandleResult(await _mediator.Send(new CreateBookCommand(dto)));
        }
    }
}
