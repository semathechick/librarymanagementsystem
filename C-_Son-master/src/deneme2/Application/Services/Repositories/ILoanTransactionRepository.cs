using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ILoanTransactionRepository : IAsyncRepository<LoanTransaction, Guid>, IRepository<LoanTransaction, Guid>
{
}