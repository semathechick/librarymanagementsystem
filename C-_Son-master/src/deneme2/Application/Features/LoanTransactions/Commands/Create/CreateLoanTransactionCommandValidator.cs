using FluentValidation;

namespace Application.Features.LoanTransactions.Commands.Create;

public class CreateLoanTransactionCommandValidator : AbstractValidator<CreateLoanTransactionCommand>
{
    public CreateLoanTransactionCommandValidator()
    {
        RuleFor(c => c.MemberId).NotEmpty();
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.ReturnStatus).NotEmpty();
        RuleFor(c => c.ReturnTime).NotEmpty();
    }
}