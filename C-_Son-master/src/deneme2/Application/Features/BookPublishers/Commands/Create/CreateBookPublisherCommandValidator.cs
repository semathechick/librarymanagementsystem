using FluentValidation;

namespace Application.Features.BookPublishers.Commands.Create;

public class CreateBookPublisherCommandValidator : AbstractValidator<CreateBookPublisherCommand>
{
    public CreateBookPublisherCommandValidator()
    {
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.PublisherId).NotEmpty();
    }
}