using Application.Features.Rezervations.Constants;
using Application.Features.Rezervations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.Rezervations.Commands.Delete;

public class DeleteRezervationCommand : IRequest<DeletedRezervationResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetRezervations"];

    public class DeleteRezervationCommandHandler : IRequestHandler<DeleteRezervationCommand, DeletedRezervationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRezervationRepository _rezervationRepository;
        private readonly RezervationBusinessRules _rezervationBusinessRules;

        public DeleteRezervationCommandHandler(IMapper mapper, IRezervationRepository rezervationRepository,
                                         RezervationBusinessRules rezervationBusinessRules)
        {
            _mapper = mapper;
            _rezervationRepository = rezervationRepository;
            _rezervationBusinessRules = rezervationBusinessRules;
        }

        public async Task<DeletedRezervationResponse> Handle(DeleteRezervationCommand request, CancellationToken cancellationToken)
        {
            Rezervation? rezervation = await _rezervationRepository.GetAsync(predicate: r => r.Id == request.Id, cancellationToken: cancellationToken);
            await _rezervationBusinessRules.RezervationShouldExistWhenSelected(rezervation);

            await _rezervationRepository.DeleteAsync(rezervation!);

            DeletedRezervationResponse response = _mapper.Map<DeletedRezervationResponse>(rezervation);
            return response;
        }
    }
}