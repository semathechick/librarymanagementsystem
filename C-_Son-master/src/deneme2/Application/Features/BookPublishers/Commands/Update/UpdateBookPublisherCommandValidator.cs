using FluentValidation;

namespace Application.Features.BookPublishers.Commands.Update;

public class UpdateBookPublisherCommandValidator : AbstractValidator<UpdateBookPublisherCommand>
{
    public UpdateBookPublisherCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.PublisherId).NotEmpty();
    }
}