using AutoMapper;
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

        public Task<Result<bool>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dto = request.Dto;

                var books_author = 
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
