using FluentValidation;

namespace Application.Features.AuthorBooks.Commands.Update;

public class UpdateAuthorBookCommandValidator : AbstractValidator<UpdateAuthorBookCommand>
{
    public UpdateAuthorBookCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.AuthorId).NotEmpty();
        RuleFor(c => c.BookId).NotEmpty();
    }
}