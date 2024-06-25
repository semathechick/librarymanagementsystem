using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;

namespace Application.Features.Categories.Queries.GetList;

public class GetListCategoryQuery : IRequest<GetListResponse<GetListCategoryListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListCategories({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetCategories";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListCategoryQueryHandler : IRequestHandler<GetListCategoryQuery, GetListResponse<GetListCategoryListItemDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetListCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListCategoryListItemDto>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Category> categories = await _categoryRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListCategoryListItemDto> response = _mapper.Map<GetListResponse<GetListCategoryListItemDto>>(categories);
            return response;
        }
    }
}