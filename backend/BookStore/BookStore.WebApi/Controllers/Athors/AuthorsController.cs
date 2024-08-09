﻿using BookStore.BLL.Dto.Author;
using BookStore.BLL.MediatR.Authors.Create;
using BookStore.BLL.MediatR.Authors.GetAll;
using BookStore.BLL.MediatR.Authors.Update;
using BookStore.WebApi.Controllers.Base;
using MediatR;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuthorDto dto)
        {
            return HandleResult(await _mediator.Send(new CreateAuthorCommand(dto)));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AuthorDto dto)
        {
            return HandleResult(await _mediator.Send(new UpdateAuthorCommand(dto)));
        }
    }
}
