using Application.Features.AuthorBooks.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.AuthorBooks.Rules;

public class AuthorBookBusinessRules : BaseBusinessRules
{
    private readonly IAuthorBookRepository _authorBookRepository;
    private readonly ILocalizationService _localizationService;

    public AuthorBookBusinessRules(IAuthorBookRepository authorBookRepository, ILocalizationService localizationService)
    {
        _authorBookRepository = authorBookRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, AuthorBooksBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task AuthorBookShouldExistWhenSelected(AuthorBook? authorBook)
    {
        if (authorBook == null)
            await throwBusinessException(AuthorBooksBusinessMessages.AuthorBookNotExists);
    }

    public async Task AuthorBookIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        AuthorBook? authorBook = await _authorBookRepository.GetAsync(
            predicate: ab => ab.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await AuthorBookShouldExistWhenSelected(authorBook);
    }
}