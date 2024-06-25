using NArchitecture.Core.Application.Responses;

namespace Application.Features.Books.Queries.GetById;

public class GetByIdBookResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ISBN { get; set; }
    public int Page { get; set; }
    public Guid CategoryId { get; set; }
    public Guid PublisherId { get; set; }
    public string Language { get; set; }
    public string Description { get; set; }
    public int UnitsInStock { get; set; }
}