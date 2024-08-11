using AutoMapper;
using BookStore.BLL.Dto.Author;
using BookStore.BLL.Dto.Book;
using BookStore.BLL.Dto.Genre;
using BookStore.DAL.Entities;
using BookStore.DAL.Repositories.Interfaces.RepositoryWrapper;
using BookStore.DAL.Repositories.Realizations.RepositoryWrapper;
using FluentResults;
using MediatR;
using MediatR.Wrappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Books.GetAll
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, Result<IEnumerable<BookDto>>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        private readonly IMapper _mapper;

        public GetAllBooksQueryHandler(IRepositoryWrapper repository, IMapper mapper)
        {
            _repositoryWrapper = repository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<BookDto>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            try
            {                                              
                var Books = (await _repositoryWrapper.BookRepository.GetAllAsync())
                .Include(b => b.Book_Authors).ThenInclude(ba => ba.Author)
                .Include(b => b.Book_Genres).ThenInclude(bg => bg.Genre);
                
                return Result.Ok(_mapper.Map<IEnumerable<BookDto>>(Books));
            }
            catch (Exception e)
            {
                return Result.Fail(new Error(e.Message));                
            }
        }
    }
}


