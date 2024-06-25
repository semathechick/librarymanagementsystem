using FluentValidation;

namespace Application.Features.Rezervations.Commands.Delete;

public class DeleteRezervationCommandValidator : AbstractValidator<DeleteRezervationCommand>
{
    public DeleteRezervationCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}