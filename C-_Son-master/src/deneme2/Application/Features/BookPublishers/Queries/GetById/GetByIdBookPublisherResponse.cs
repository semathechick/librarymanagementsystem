using NArchitecture.Core.Application.Responses;

namespace Application.Features.BookPublishers.Queries.GetById;

public class GetByIdBookPublisherResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid PublisherId { get; set; }
}