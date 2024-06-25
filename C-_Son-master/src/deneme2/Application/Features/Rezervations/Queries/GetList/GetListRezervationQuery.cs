using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Rezervations.Queries.GetList;

public class GetListRezervationQuery : IRequest<GetListResponse<GetListRezervationListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }
    public Guid MemberId { get; set; }
    public bool BypassCache { get; }
    public string? CacheKey => $"GetListRezervations({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetRezervations";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListRezervationQueryHandler : IRequestHandler<GetListRezervationQuery, GetListResponse<GetListRezervationListItemDto>>
    {
        private readonly IRezervationRepository _rezervationRepository;
        private readonly IMapper _mapper;

        public GetListRezervationQueryHandler(IRezervationRepository rezervationRepository, IMapper mapper)
        {
            _rezervationRepository = rezervationRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListRezervationListItemDto>> Handle(GetListRezervationQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Rezervation> rezervations = await _rezervationRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, include: i => i.Include(r => r.Book),
                cancellationToken: cancellationToken
               
            );

            GetListResponse<GetListRezervationListItemDto> response = _mapper.Map<GetListResponse<GetListRezervationListItemDto>>(rezervations);
            return response;
        }
    }
}