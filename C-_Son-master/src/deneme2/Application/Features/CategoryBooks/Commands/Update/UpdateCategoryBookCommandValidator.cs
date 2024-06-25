using FluentValidation;

namespace Application.Features.CategoryBooks.Commands.Update;

public class UpdateCategoryBookCommandValidator : AbstractValidator<UpdateCategoryBookCommand>
{
    public UpdateCategoryBookCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.BookdId).NotEmpty();
        RuleFor(c => c.CategoryId).NotEmpty();
    }
}