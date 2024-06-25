using FluentValidation;

namespace Application.Features.BookPublishers.Commands.Delete;

public class DeleteBookPublisherCommandValidator : AbstractValidator<DeleteBookPublisherCommand>
{
    public DeleteBookPublisherCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}