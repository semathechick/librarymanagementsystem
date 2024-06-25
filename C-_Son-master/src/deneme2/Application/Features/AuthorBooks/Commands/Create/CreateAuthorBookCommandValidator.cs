using FluentValidation;

namespace Application.Features.AuthorBooks.Commands.Create;

public class CreateAuthorBookCommandValidator : AbstractValidator<CreateAuthorBookCommand>
{
    public CreateAuthorBookCommandValidator()
    {
        RuleFor(c => c.AuthorId).NotEmpty();
        RuleFor(c => c.BookId).NotEmpty();
    }
}