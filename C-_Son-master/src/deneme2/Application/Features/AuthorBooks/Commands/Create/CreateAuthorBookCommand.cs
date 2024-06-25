using Application.Features.AuthorBooks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.AuthorBooks.Commands.Create;

public class CreateAuthorBookCommand : IRequest<CreatedAuthorBookResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid AuthorId { get; set; }
    public Guid BookId { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetAuthorBooks"];

    public class CreateAuthorBookCommandHandler : IRequestHandler<CreateAuthorBookCommand, CreatedAuthorBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthorBookRepository _authorBookRepository;
        private readonly AuthorBookBusinessRules _authorBookBusinessRules;

        public CreateAuthorBookCommandHandler(IMapper mapper, IAuthorBookRepository authorBookRepository,
                                         AuthorBookBusinessRules authorBookBusinessRules)
        {
            _mapper = mapper;
            _authorBookRepository = authorBookRepository;
            _authorBookBusinessRules = authorBookBusinessRules;
        }

        public async Task<CreatedAuthorBookResponse> Handle(CreateAuthorBookCommand request, CancellationToken cancellationToken)
        {
            AuthorBook authorBook = _mapper.Map<AuthorBook>(request);

            await _authorBookRepository.AddAsync(authorBook);

            CreatedAuthorBookResponse response = _mapper.Map<CreatedAuthorBookResponse>(authorBook);
            return response;
        }
    }
}