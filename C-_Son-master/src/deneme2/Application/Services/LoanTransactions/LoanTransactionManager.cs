using Application.Features.LoanTransactions.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.LoanTransactions;

public class LoanTransactionManager : ILoanTransactionService
{
    private readonly ILoanTransactionRepository _loanTransactionRepository;
    private readonly LoanTransactionBusinessRules _loanTransactionBusinessRules;

    public LoanTransactionManager(ILoanTransactionRepository loanTransactionRepository, LoanTransactionBusinessRules loanTransactionBusinessRules)
    {
        _loanTransactionRepository = loanTransactionRepository;
        _loanTransactionBusinessRules = loanTransactionBusinessRules;
    }

    public async Task<LoanTransaction?> GetAsync(
        Expression<Func<LoanTransaction, bool>> predicate,
        Func<IQueryable<LoanTransaction>, IIncludableQueryable<LoanTransaction, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        LoanTransaction? loanTransaction = await _loanTransactionRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return loanTransaction;
    }

    public async Task<IPaginate<LoanTransaction>?> GetListAsync(
        Expression<Func<LoanTransaction, bool>>? predicate = null,
        Func<IQueryable<LoanTransaction>, IOrderedQueryable<LoanTransaction>>? orderBy = null,
        Func<IQueryable<LoanTransaction>, IIncludableQueryable<LoanTransaction, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<LoanTransaction> loanTransactionList = await _loanTransactionRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return loanTransactionList;
    }

    public async Task<LoanTransaction> AddAsync(LoanTransaction loanTransaction)
    {
        LoanTransaction addedLoanTransaction = await _loanTransactionRepository.AddAsync(loanTransaction);

        return addedLoanTransaction;
    }

    public async Task<LoanTransaction> UpdateAsync(LoanTransaction loanTransaction)
    {
        LoanTransaction updatedLoanTransaction = await _loanTransactionRepository.UpdateAsync(loanTransaction);

        return updatedLoanTransaction;
    }

    public async Task<LoanTransaction> DeleteAsync(LoanTransaction loanTransaction, bool permanent = false)
    {
        LoanTransaction deletedLoanTransaction = await _loanTransactionRepository.DeleteAsync(loanTransaction);

        return deletedLoanTransaction;
    }
}
