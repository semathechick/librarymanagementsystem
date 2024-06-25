using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;

namespace Application.Features.CategoryBooks.Queries.GetList;

public class GetListCategoryBookQuery : IRequest<GetListResponse<GetListCategoryBookListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListCategoryBooks({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetCategoryBooks";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListCategoryBookQueryHandler : IRequestHandler<GetListCategoryBookQuery, GetListResponse<GetListCategoryBookListItemDto>>
    {
        private readonly ICategoryBookRepository _categoryBookRepository;
        private readonly IMapper _mapper;

        public GetListCategoryBookQueryHandler(ICategoryBookRepository categoryBookRepository, IMapper mapper)
        {
            _categoryBookRepository = categoryBookRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListCategoryBookListItemDto>> Handle(GetListCategoryBookQuery request, CancellationToken cancellationToken)
        {
            IPaginate<CategoryBook> categoryBooks = await _categoryBookRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListCategoryBookListItemDto> response = _mapper.Map<GetListResponse<GetListCategoryBookListItemDto>>(categoryBooks);
            return response;
        }
    }
}