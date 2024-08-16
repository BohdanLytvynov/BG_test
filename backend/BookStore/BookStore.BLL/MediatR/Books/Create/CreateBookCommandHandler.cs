using AutoMapper;
using AutoMapper.Configuration.Annotations;
using BookStore.DAL.Entities;
using BookStore.DAL.Repositories.Interfaces.RepositoryWrapper;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Books.Create
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result<bool>>
    {
        IRepositoryWrapper _repositoryWrapper;

        IMapper _mapper;

        public CreateBookCommandHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;

            _mapper = mapper;
        }

        public async Task<Result<bool>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var bookRepo = _repositoryWrapper.BookRepository;

                bookRepo.AddBook(new Book()
                { 
                    Name = request.Dto.Name,
                    PubYear = request.Dto.PubYear,                    
                }, _mapper.Map<IEnumerable<Author>>(request.Dto.Authors),
                    _mapper.Map<IEnumerable<Genre>>(request.Dto.Genres));

                if (await _repositoryWrapper.SaveChangesAsync() > 0)
                {
                    return Result.Ok(true);
                }
                throw new Exception("Something goes wrong when trying to save new Book!");
            }
            catch (Exception e)
            {
                return Result.Fail(new Error(e.Message));               
            }
        }
    }
}
