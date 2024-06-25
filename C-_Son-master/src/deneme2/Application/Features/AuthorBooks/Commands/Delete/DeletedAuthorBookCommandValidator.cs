using FluentValidation;

namespace Application.Features.AuthorBooks.Commands.Delete;

public class DeleteAuthorBookCommandValidator : AbstractValidator<DeleteAuthorBookCommand>
{
    public DeleteAuthorBookCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}