using AutoMapper;
using BookStore.BLL.Dto.Author;
using BookStore.DAL.Repositories.Interfaces.RepositoryWrapper;
using FluentResults;
using MediatR;

namespace BookStore.BLL.MediatR.Authors.GetAll
{
    public class GetAllAuthorsHandler : IRequestHandler<GetAllAuthorsQuery, Result<IEnumerable<AuthorDto>>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public GetAllAuthorsHandler(IRepositoryWrapper repositoryWrapper,
            IMapper mapper)
        {
            _mapper = mapper;

            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Result<IEnumerable<AuthorDto>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors = await _repositoryWrapper.AuthorRepository.GetAllAsync();
            
            return Result.Ok(_mapper.Map<IEnumerable<AuthorDto>>(authors));
        }
    }
}
