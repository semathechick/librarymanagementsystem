using FluentValidation;

namespace Application.Features.CategoryBooks.Commands.Delete;

public class DeleteCategoryBookCommandValidator : AbstractValidator<DeleteCategoryBookCommand>
{
    public DeleteCategoryBookCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}