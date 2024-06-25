using Application.Features.Publishers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.Publishers.Commands.Update;

public class UpdatePublisherCommand : IRequest<UpdatedPublisherResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetPublishers"];

    public class UpdatePublisherCommandHandler : IRequestHandler<UpdatePublisherCommand, UpdatedPublisherResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPublisherRepository _publisherRepository;
        private readonly PublisherBusinessRules _publisherBusinessRules;

        public UpdatePublisherCommandHandler(IMapper mapper, IPublisherRepository publisherRepository,
                                         PublisherBusinessRules publisherBusinessRules)
        {
            _mapper = mapper;
            _publisherRepository = publisherRepository;
            _publisherBusinessRules = publisherBusinessRules;
        }

        public async Task<UpdatedPublisherResponse> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
        {
            Publisher? publisher = await _publisherRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _publisherBusinessRules.PublisherShouldExistWhenSelected(publisher);
            publisher = _mapper.Map(request, publisher);

            await _publisherRepository.UpdateAsync(publisher!);

            UpdatedPublisherResponse response = _mapper.Map<UpdatedPublisherResponse>(publisher);
            return response;
        }
    }
}