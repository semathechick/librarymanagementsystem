using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.Categories.Commands.Create;

public class CreateCategoryCommand : IRequest<CreatedCategoryResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string CategoryName { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCategories"];

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreatedCategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryBusinessRules _categoryBusinessRules;

        public CreateCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository,
                                         CategoryBusinessRules categoryBusinessRules)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _categoryBusinessRules = categoryBusinessRules;
        }

        public async Task<CreatedCategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            await _categoryBusinessRules.CategoryShouldBeNotExists(request.CategoryName);
            Category category = _mapper.Map<Category>(request);

            await _categoryRepository.AddAsync(category);

            CreatedCategoryResponse response = _mapper.Map<CreatedCategoryResponse>(category);
            return response;
        }
    }
}