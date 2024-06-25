using Application.Features.BookPublishers.Constants;
using Application.Features.BookPublishers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.BookPublishers.Commands.Delete;

public class DeleteBookPublisherCommand : IRequest<DeletedBookPublisherResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBookPublishers"];

    public class DeleteBookPublisherCommandHandler : IRequestHandler<DeleteBookPublisherCommand, DeletedBookPublisherResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBookPublisherRepository _bookPublisherRepository;
        private readonly BookPublisherBusinessRules _bookPublisherBusinessRules;

        public DeleteBookPublisherCommandHandler(IMapper mapper, IBookPublisherRepository bookPublisherRepository,
                                         BookPublisherBusinessRules bookPublisherBusinessRules)
        {
            _mapper = mapper;
            _bookPublisherRepository = bookPublisherRepository;
            _bookPublisherBusinessRules = bookPublisherBusinessRules;
        }

        public async Task<DeletedBookPublisherResponse> Handle(DeleteBookPublisherCommand request, CancellationToken cancellationToken)
        {
            BookPublisher? bookPublisher = await _bookPublisherRepository.GetAsync(predicate: bp => bp.Id == request.Id, cancellationToken: cancellationToken);
            await _bookPublisherBusinessRules.BookPublisherShouldExistWhenSelected(bookPublisher);

            await _bookPublisherRepository.DeleteAsync(bookPublisher!);

            DeletedBookPublisherResponse response = _mapper.Map<DeletedBookPublisherResponse>(bookPublisher);
            return response;
        }
    }
}