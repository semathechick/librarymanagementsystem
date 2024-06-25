using FluentValidation;

namespace Application.Features.Rezervations.Commands.Create;

public class CreateRezervationCommandValidator : AbstractValidator<CreateRezervationCommand>
{
    public CreateRezervationCommandValidator()
    {
        
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.MemberId).NotEmpty();
        RuleFor(c => c.RezervationDate).NotEmpty();
        RuleFor(c => c.ExpirationDate).NotEmpty();
    }
}