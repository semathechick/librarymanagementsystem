using Application.Features.Books.Queries.GetBookByCategoryId;
using Application.Features.Books.Queries.GetList;
using Application.Features.Rezervations.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Books.Queries.GetBookByCategoryId.GetListBookByCategoryIdQuery;

namespace Application.Features.Rezervations.Queries.GetListReservationByMemberId;
public class GetListReservationByMemberId : IRequest<GetListResponse<GetListRezervationListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public Guid MemberId { get; set; }
    public bool BypassCache { get; }
    public TimeSpan? SlidingExpiration { get; }

    public class GetListReservationByMemberIdHandler : IRequestHandler<GetListReservationByMemberId, GetListResponse<GetListRezervationListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRezervationRepository _rezervationRepository;
        public GetListReservationByMemberIdHandler(IRezervationRepository rezervationRepository, IMapper mapper)
        {
            _rezervationRepository = rezervationRepository;
            _mapper = mapper;
        }
        public async Task<GetListResponse<GetListRezervationListItemDto>> Handle(
            GetListReservationByMemberId request,
            CancellationToken cancellationToken)
        {
            IPaginate<Rezervation> reservations = await _rezervationRepository.GetListAsync(
                predicate: x => x.MemberId == request.MemberId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, include: i => i.Include(r => r.Book),
                cancellationToken: cancellationToken
            ) ;
            GetListResponse<GetListRezervationListItemDto> response = _mapper.Map<GetListResponse<GetListRezervationListItemDto>>(reservations);
            return response;
        }
    }
}
