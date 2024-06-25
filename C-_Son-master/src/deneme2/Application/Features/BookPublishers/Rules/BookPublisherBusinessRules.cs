using Application.Features.BookPublishers.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.BookPublishers.Rules;

public class BookPublisherBusinessRules : BaseBusinessRules
{
    private readonly IBookPublisherRepository _bookPublisherRepository;
    private readonly ILocalizationService _localizationService;

    public BookPublisherBusinessRules(IBookPublisherRepository bookPublisherRepository, ILocalizationService localizationService)
    {
        _bookPublisherRepository = bookPublisherRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, BookPublishersBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task BookPublisherShouldExistWhenSelected(BookPublisher? bookPublisher)
    {
        if (bookPublisher == null)
            await throwBusinessException(BookPublishersBusinessMessages.BookPublisherNotExists);
    }

    public async Task BookPublisherIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        BookPublisher? bookPublisher = await _bookPublisherRepository.GetAsync(
            predicate: bp => bp.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await BookPublisherShouldExistWhenSelected(bookPublisher);
    }
}