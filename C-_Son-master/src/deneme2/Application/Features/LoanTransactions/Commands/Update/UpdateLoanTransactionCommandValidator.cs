using FluentValidation;

namespace Application.Features.LoanTransactions.Commands.Update;

public class UpdateLoanTransactionCommandValidator : AbstractValidator<UpdateLoanTransactionCommand>
{
    public UpdateLoanTransactionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.MemberId).NotEmpty();
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.ReturnStatus).NotEmpty();
       
    }
}