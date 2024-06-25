using Application.Features.Rezervations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.Rezervations.Commands.Update;

public class UpdateRezervationCommand : IRequest<UpdatedRezervationResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid MemberId { get; set; }
    public DateTime RezervationDate { get; set; }
    public DateTime ExpirationDate { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetRezervations"];

    public class UpdateRezervationCommandHandler : IRequestHandler<UpdateRezervationCommand, UpdatedRezervationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRezervationRepository _rezervationRepository;
        private readonly RezervationBusinessRules _rezervationBusinessRules;

        public UpdateRezervationCommandHandler(IMapper mapper, IRezervationRepository rezervationRepository,
                                         RezervationBusinessRules rezervationBusinessRules)
        {
            _mapper = mapper;
            _rezervationRepository = rezervationRepository;
            _rezervationBusinessRules = rezervationBusinessRules;
        }

        public async Task<UpdatedRezervationResponse> Handle(UpdateRezervationCommand request, CancellationToken cancellationToken)
        {
            Rezervation? rezervation = await _rezervationRepository.GetAsync(predicate: r => r.Id == request.Id, cancellationToken: cancellationToken);
            await _rezervationBusinessRules.RezervationShouldExistWhenSelected(rezervation);
            rezervation = _mapper.Map(request, rezervation);

            await _rezervationRepository.UpdateAsync(rezervation!);

            UpdatedRezervationResponse response = _mapper.Map<UpdatedRezervationResponse>(rezervation);
            return response;
        }
    }
}