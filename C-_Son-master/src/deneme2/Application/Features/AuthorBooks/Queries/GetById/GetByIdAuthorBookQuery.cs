using Application.Features.AuthorBooks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.AuthorBooks.Queries.GetById;

public class GetByIdAuthorBookQuery : IRequest<GetByIdAuthorBookResponse>
{
    public Guid Id { get; set; }

    public class GetByIdAuthorBookQueryHandler : IRequestHandler<GetByIdAuthorBookQuery, GetByIdAuthorBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthorBookRepository _authorBookRepository;
        private readonly AuthorBookBusinessRules _authorBookBusinessRules;

        public GetByIdAuthorBookQueryHandler(IMapper mapper, IAuthorBookRepository authorBookRepository, AuthorBookBusinessRules authorBookBusinessRules)
        {
            _mapper = mapper;
            _authorBookRepository = authorBookRepository;
            _authorBookBusinessRules = authorBookBusinessRules;
        }

        public async Task<GetByIdAuthorBookResponse> Handle(GetByIdAuthorBookQuery request, CancellationToken cancellationToken)
        {
            AuthorBook? authorBook = await _authorBookRepository.GetAsync(predicate: ab => ab.Id == request.Id, cancellationToken: cancellationToken);
            await _authorBookBusinessRules.AuthorBookShouldExistWhenSelected(authorBook);

            GetByIdAuthorBookResponse response = _mapper.Map<GetByIdAuthorBookResponse>(authorBook);
            return response;
        }
    }
}