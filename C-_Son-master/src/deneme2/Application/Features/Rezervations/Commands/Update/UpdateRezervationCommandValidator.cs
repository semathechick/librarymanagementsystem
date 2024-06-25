using FluentValidation;

namespace Application.Features.Rezervations.Commands.Update;

public class UpdateRezervationCommandValidator : AbstractValidator<UpdateRezervationCommand>
{
    public UpdateRezervationCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.MemberId).NotEmpty();
        RuleFor(c => c.RezervationDate).NotEmpty();
        RuleFor(c => c.ExpirationDate).NotEmpty();
    }
}