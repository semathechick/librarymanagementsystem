using Application.Features.Publishers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.Publishers.Commands.Create;

public class CreatePublisherCommand : IRequest<CreatedPublisherResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetPublishers"];

    public class CreatePublisherCommandHandler : IRequestHandler<CreatePublisherCommand, CreatedPublisherResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPublisherRepository _publisherRepository;
        private readonly PublisherBusinessRules _publisherBusinessRules;

        public CreatePublisherCommandHandler(IMapper mapper, IPublisherRepository publisherRepository,
                                         PublisherBusinessRules publisherBusinessRules)
        {
            _mapper = mapper;
            _publisherRepository = publisherRepository;
            _publisherBusinessRules = publisherBusinessRules;
        }

        public async Task<CreatedPublisherResponse> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
        {
            await _publisherBusinessRules.PublisherShouldBeNotExists(request.Name);
            Publisher publisher = _mapper.Map<Publisher>(request);

            await _publisherRepository.AddAsync(publisher);

            CreatedPublisherResponse response = _mapper.Map<CreatedPublisherResponse>(publisher);
            return response;
        }
    }
}