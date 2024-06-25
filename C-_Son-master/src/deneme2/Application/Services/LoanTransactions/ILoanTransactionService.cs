using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.LoanTransactions;

public interface ILoanTransactionService
{
    Task<LoanTransaction?> GetAsync(
        Expression<Func<LoanTransaction, bool>> predicate,
        Func<IQueryable<LoanTransaction>, IIncludableQueryable<LoanTransaction, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<LoanTransaction>?> GetListAsync(
        Expression<Func<LoanTransaction, bool>>? predicate = null,
        Func<IQueryable<LoanTransaction>, IOrderedQueryable<LoanTransaction>>? orderBy = null,
        Func<IQueryable<LoanTransaction>, IIncludableQueryable<LoanTransaction, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<LoanTransaction> AddAsync(LoanTransaction loanTransaction);
    Task<LoanTransaction> UpdateAsync(LoanTransaction loanTransaction);
    Task<LoanTransaction> DeleteAsync(LoanTransaction loanTransaction, bool permanent = false);
}
