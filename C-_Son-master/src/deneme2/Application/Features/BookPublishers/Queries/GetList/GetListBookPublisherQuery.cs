using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;

namespace Application.Features.BookPublishers.Queries.GetList;

public class GetListBookPublisherQuery : IRequest<GetListResponse<GetListBookPublisherListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListBookPublishers({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetBookPublishers";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListBookPublisherQueryHandler : IRequestHandler<GetListBookPublisherQuery, GetListResponse<GetListBookPublisherListItemDto>>
    {
        private readonly IBookPublisherRepository _bookPublisherRepository;
        private readonly IMapper _mapper;

        public GetListBookPublisherQueryHandler(IBookPublisherRepository bookPublisherRepository, IMapper mapper)
        {
            _bookPublisherRepository = bookPublisherRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListBookPublisherListItemDto>> Handle(GetListBookPublisherQuery request, CancellationToken cancellationToken)
        {
            IPaginate<BookPublisher> bookPublishers = await _bookPublisherRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListBookPublisherListItemDto> response = _mapper.Map<GetListResponse<GetListBookPublisherListItemDto>>(bookPublishers);
            return response;
        }
    }
}