using Application.Features.Categories.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;
using Application.Features.Books.Constants;

namespace Application.Features.Categories.Rules;

public class CategoryBusinessRules : BaseBusinessRules
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILocalizationService _localizationService;

    public CategoryBusinessRules(ICategoryRepository categoryRepository, ILocalizationService localizationService)
    {
        _categoryRepository = categoryRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, CategoriesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task CategoryShouldExistWhenSelected(Category? category)
    {
        if (category == null)
            await throwBusinessException(CategoriesBusinessMessages.CategoryNotExists);
    }
    public async Task CategoryShouldBeNotExists(string categoryName)
    {
        bool doesExists = await _categoryRepository.AnyAsync(predicate: u => u.CategoryName == categoryName);
        if (doesExists)
            await throwBusinessException(CategoriesBusinessMessages.CategoryAlreadyExist);
    }
    public async Task CategoryIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Category? category = await _categoryRepository.GetAsync(
            predicate: c => c.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await CategoryShouldExistWhenSelected(category);
    }
}