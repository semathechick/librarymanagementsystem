using Application.Features.LoanTransactions.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.LoanTransactions.Rules;

public class LoanTransactionBusinessRules : BaseBusinessRules
{
    private readonly ILoanTransactionRepository _loanTransactionRepository;
    private readonly ILocalizationService _localizationService;

    public LoanTransactionBusinessRules(ILoanTransactionRepository loanTransactionRepository, ILocalizationService localizationService)
    {
        _loanTransactionRepository = loanTransactionRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, LoanTransactionsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task LoanTransactionShouldExistWhenSelected(LoanTransaction? loanTransaction)
    {
        if (loanTransaction == null)
            await throwBusinessException(LoanTransactionsBusinessMessages.LoanTransactionNotExists);
    }

    public async Task CheckIfBookPreviouslyBorrowed(Guid? loanBookId)
    {//Kitap daha önce ödünç alýndý mý
        LoanTransaction? loan = await _loanTransactionRepository.GetAsync(predicate: book => book.BookId == loanBookId);
        if (loan is not null)
            await throwBusinessException(LoanTransactionsBusinessMessages.CheckIfBookPreviouslyBorrowed);
    }
    public async Task LoanTransactionIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        LoanTransaction? loanTransaction = await _loanTransactionRepository.GetAsync(
            predicate: lt => lt.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await LoanTransactionShouldExistWhenSelected(loanTransaction);
    }
}