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
                var temp = new List<BookDto>();

                var Books_Author = await _repositoryWrapper.BookRepository
                   .GetQueryableSet<Book_Author>(null, include => include.Include(x => x.Author)
                   .Include(x => x.Book));

                var Books_Gener = await _repositoryWrapper.BookRepository
                   .GetQueryableSet<Book_Genre>(null, include => include.Include(x => x.Book)
                   .Include(x => x.Genre));

                var Authors = await _repositoryWrapper.AuthorRepository.GetAllAsync();
                var Geners = await _repositoryWrapper.GenreRepository.GetAllAsync();

                temp = Books_Author.Join(Books_Gener, x => x.BookId, y => y.BookId,
                    (x, y) => new BookDto() 
                    {
                        Name = x.Book.Name,
                        Id = x.Book.Id,
                        PubYear = x.Book.PubYear,
                        Authors = _mapper.Map<List<AuthorDto>>(
                            Authors.Where(a => a.Id == x.AuthorId)),
                        Genres = _mapper.Map<List<GenreDto>>(
                            Geners.Where(g => g.Id == y.GenreId)
                            )
                    }
                    ).ToList();

                return Result.Ok(temp as IEnumerable<BookDto>);
            }
            catch (Exception e)
            {
                return Result.Fail(new Error(e.Message));                
            }
        }
    }
}


