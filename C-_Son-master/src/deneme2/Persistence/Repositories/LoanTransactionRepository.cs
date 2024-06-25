using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class LoanTransactionRepository : EfRepositoryBase<LoanTransaction, Guid, BaseDbContext>, ILoanTransactionRepository
{
    public LoanTransactionRepository(BaseDbContext context) : base(context)
    {
    }
}