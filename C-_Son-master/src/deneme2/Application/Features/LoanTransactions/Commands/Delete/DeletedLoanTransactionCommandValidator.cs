using FluentValidation;

namespace Application.Features.LoanTransactions.Commands.Delete;

public class DeleteLoanTransactionCommandValidator : AbstractValidator<DeleteLoanTransactionCommand>
{
    public DeleteLoanTransactionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}