using Application.Features.Rezervations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.Rezervations.Commands.Create;

public class CreateRezervationCommand : IRequest<CreatedRezervationResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    
    public Guid BookId { get; set; }
    public Guid MemberId { get; set; }
    public DateTime RezervationDate { get; set; }
    public DateTime ExpirationDate { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetRezervations"];

    public class CreateRezervationCommandHandler : IRequestHandler<CreateRezervationCommand, CreatedRezervationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRezervationRepository _rezervationRepository;
        private readonly RezervationBusinessRules _rezervationBusinessRules;

        public CreateRezervationCommandHandler(IMapper mapper, IRezervationRepository rezervationRepository,
                                         RezervationBusinessRules rezervationBusinessRules)
        {
            _mapper = mapper;
            _rezervationRepository = rezervationRepository;
            _rezervationBusinessRules = rezervationBusinessRules;
        }

        public async Task<CreatedRezervationResponse> Handle(CreateRezervationCommand request, CancellationToken cancellationToken)
        {
            Rezervation rezervation = _mapper.Map<Rezervation>(request);

            await _rezervationRepository.AddAsync(rezervation);

            CreatedRezervationResponse response = _mapper.Map<CreatedRezervationResponse>(rezervation);
            return response;
        }
    }
}