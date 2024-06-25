using FluentValidation;

namespace Application.Features.Books.Commands.Create;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.ISBN).NotEmpty();
    }
}