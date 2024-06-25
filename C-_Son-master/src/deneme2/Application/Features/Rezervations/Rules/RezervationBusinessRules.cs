using Application.Features.Rezervations.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Rezervations.Rules;

public class RezervationBusinessRules : BaseBusinessRules
{
    private readonly IRezervationRepository _rezervationRepository;
    private readonly ILocalizationService _localizationService;

    public RezervationBusinessRules(IRezervationRepository rezervationRepository, ILocalizationService localizationService)
    {
        _rezervationRepository = rezervationRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, RezervationsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task RezervationShouldExistWhenSelected(Rezervation? rezervation)
    {
        if (rezervation == null)
            await throwBusinessException(RezervationsBusinessMessages.RezervationNotExists);
    }

    public async Task RezervationIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Rezervation? rezervation = await _rezervationRepository.GetAsync(
            predicate: r => r.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await RezervationShouldExistWhenSelected(rezervation);
    }
}