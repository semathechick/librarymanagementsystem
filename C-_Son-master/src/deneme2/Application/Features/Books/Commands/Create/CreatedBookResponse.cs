using NArchitecture.Core.Application.Responses;

namespace Application.Features.Books.Commands.Create;

public class CreatedBookResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ISBN { get; set; }
    public int Page { get; set; }
    public string Language { get; set; }
    public int UnitsInStock { get; set; }
    public string Description { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }

    public Guid PublisherId { get; set; }
    public string PublisherName { get; set; }
}