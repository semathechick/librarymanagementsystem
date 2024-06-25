using Application.Features.AuthorBooks.Constants;
using Application.Features.AuthorBooks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.AuthorBooks.Commands.Delete;

public class DeleteAuthorBookCommand : IRequest<DeletedAuthorBookResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetAuthorBooks"];

    public class DeleteAuthorBookCommandHandler : IRequestHandler<DeleteAuthorBookCommand, DeletedAuthorBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthorBookRepository _authorBookRepository;
        private readonly AuthorBookBusinessRules _authorBookBusinessRules;

        public DeleteAuthorBookCommandHandler(IMapper mapper, IAuthorBookRepository authorBookRepository,
                                         AuthorBookBusinessRules authorBookBusinessRules)
        {
            _mapper = mapper;
            _authorBookRepository = authorBookRepository;
            _authorBookBusinessRules = authorBookBusinessRules;
        }

        public async Task<DeletedAuthorBookResponse> Handle(DeleteAuthorBookCommand request, CancellationToken cancellationToken)
        {
            AuthorBook? authorBook = await _authorBookRepository.GetAsync(predicate: ab => ab.Id == request.Id, cancellationToken: cancellationToken);
            await _authorBookBusinessRules.AuthorBookShouldExistWhenSelected(authorBook);

            await _authorBookRepository.DeleteAsync(authorBook!);

            DeletedAuthorBookResponse response = _mapper.Map<DeletedAuthorBookResponse>(authorBook);
            return response;
        }
    }
}