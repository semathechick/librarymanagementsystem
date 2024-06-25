using Application.Features.Rezervations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Rezervations.Queries.GetById;

public class GetByIdRezervationQuery : IRequest<GetByIdRezervationResponse>
{
    public Guid Id { get; set; }

    public class GetByIdRezervationQueryHandler : IRequestHandler<GetByIdRezervationQuery, GetByIdRezervationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRezervationRepository _rezervationRepository;
        private readonly RezervationBusinessRules _rezervationBusinessRules;

        public GetByIdRezervationQueryHandler(IMapper mapper, IRezervationRepository rezervationRepository, RezervationBusinessRules rezervationBusinessRules)
        {
            _mapper = mapper;
            _rezervationRepository = rezervationRepository;
            _rezervationBusinessRules = rezervationBusinessRules;
        }

        public async Task<GetByIdRezervationResponse> Handle(GetByIdRezervationQuery request, CancellationToken cancellationToken)
        {
            Rezervation? rezervation = await _rezervationRepository.GetAsync(predicate: r => r.Id == request.Id, cancellationToken: cancellationToken);
            await _rezervationBusinessRules.RezervationShouldExistWhenSelected(rezervation);

            GetByIdRezervationResponse response = _mapper.Map<GetByIdRezervationResponse>(rezervation);
            return response;
        }
    }
}