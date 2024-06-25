using NArchitecture.Core.Application.Responses;

namespace Application.Features.BookPublishers.Commands.Update;

public class UpdatedBookPublisherResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid PublisherId { get; set; }
}