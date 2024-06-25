using FluentValidation;

namespace Application.Features.CategoryBooks.Commands.Create;

public class CreateCategoryBookCommandValidator : AbstractValidator<CreateCategoryBookCommand>
{
    public CreateCategoryBookCommandValidator()
    {
        RuleFor(c => c.BookdId).NotEmpty();
        RuleFor(c => c.CategoryId).NotEmpty();
    }
}