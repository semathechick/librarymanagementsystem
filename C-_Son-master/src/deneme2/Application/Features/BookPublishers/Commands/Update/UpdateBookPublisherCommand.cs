using Application.Features.BookPublishers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.BookPublishers.Commands.Update;

public class UpdateBookPublisherCommand : IRequest<UpdatedBookPublisherResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid PublisherId { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBookPublishers"];

    public class UpdateBookPublisherCommandHandler : IRequestHandler<UpdateBookPublisherCommand, UpdatedBookPublisherResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBookPublisherRepository _bookPublisherRepository;
        private readonly BookPublisherBusinessRules _bookPublisherBusinessRules;

        public UpdateBookPublisherCommandHandler(IMapper mapper, IBookPublisherRepository bookPublisherRepository,
                                         BookPublisherBusinessRules bookPublisherBusinessRules)
        {
            _mapper = mapper;
            _bookPublisherRepository = bookPublisherRepository;
            _bookPublisherBusinessRules = bookPublisherBusinessRules;
        }

        public async Task<UpdatedBookPublisherResponse> Handle(UpdateBookPublisherCommand request, CancellationToken cancellationToken)
        {
            BookPublisher? bookPublisher = await _bookPublisherRepository.GetAsync(predicate: bp => bp.Id == request.Id, cancellationToken: cancellationToken);
            await _bookPublisherBusinessRules.BookPublisherShouldExistWhenSelected(bookPublisher);
            bookPublisher = _mapper.Map(request, bookPublisher);

            await _bookPublisherRepository.UpdateAsync(bookPublisher!);

            UpdatedBookPublisherResponse response = _mapper.Map<UpdatedBookPublisherResponse>(bookPublisher);
            return response;
        }
    }
}