using Application.Features.Books.Constants;
using Application.Features.Books.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.Books.Commands.Delete;

public class DeleteBookCommand : IRequest<DeletedBookResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBooks"];

    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, DeletedBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        private readonly BookBusinessRules _bookBusinessRules;

        public DeleteBookCommandHandler(IMapper mapper, IBookRepository bookRepository,
                                         BookBusinessRules bookBusinessRules)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
            _bookBusinessRules = bookBusinessRules;
        }

        public async Task<DeletedBookResponse> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            Book? book = await _bookRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
            await _bookBusinessRules.BookShouldExistWhenSelected(book);

            await _bookRepository.DeleteAsync(book!);

            DeletedBookResponse response = _mapper.Map<DeletedBookResponse>(book);
            return response;
        }
    }
}