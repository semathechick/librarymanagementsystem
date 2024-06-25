using NArchitecture.Core.Application.Responses;

namespace Application.Features.LoanTransactions.Commands.Delete;

public class DeletedLoanTransactionResponse : IResponse
{
    public Guid Id { get; set; }
}