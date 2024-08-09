using AutoMapper;
using BookStore.DAL.Entities;
using BookStore.DAL.Repositories.Interfaces.RepositoryWrapper;
using FluentResults;
using MediatR;

namespace BookStore.BLL.MediatR.Authors.Create
{
    public class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, Result<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repository;

        public CreateAuthorHandler(
            IRepositoryWrapper repositoryWrapper,
            IMapper mapper)
        {
            _mapper = mapper;
            _repository = repositoryWrapper;
        }

        public async Task<Result<bool>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {            
            try
            {
                var repo = _repository.AuthorRepository;

                repo.Create(_mapper.Map<Author>(request.dto));
                
                if(await _repository.SaveChangesAsync() > 0)
                    return Result.Ok(true);
                return Result.Fail(new Error("Fail to update Entity!"));

            }
            catch (Exception e)
            {
                return Result.Fail(new Error(e.Message));            
            }
            
        }
    }
}
