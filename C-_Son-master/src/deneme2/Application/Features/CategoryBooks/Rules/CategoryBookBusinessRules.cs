using Application.Features.CategoryBooks.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.CategoryBooks.Rules;

public class CategoryBookBusinessRules : BaseBusinessRules
{
    private readonly ICategoryBookRepository _categoryBookRepository;
    private readonly ILocalizationService _localizationService;

    public CategoryBookBusinessRules(ICategoryBookRepository categoryBookRepository, ILocalizationService localizationService)
    {
        _categoryBookRepository = categoryBookRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, CategoryBooksBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task CategoryBookShouldExistWhenSelected(CategoryBook? categoryBook)
    {
        if (categoryBook == null)
            await throwBusinessException(CategoryBooksBusinessMessages.CategoryBookNotExists);
    }

    public async Task CategoryBookIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        CategoryBook? categoryBook = await _categoryBookRepository.GetAsync(
            predicate: cb => cb.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await CategoryBookShouldExistWhenSelected(categoryBook);
    }
}