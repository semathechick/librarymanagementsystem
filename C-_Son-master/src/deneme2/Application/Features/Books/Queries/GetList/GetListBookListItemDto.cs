using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Books.Queries.GetList;

public class GetListBookListItemDto : IDto
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
    public Guid AuthorId { get; set; }
    public string AuthorName { get; set; }
    public Guid PublisherId { get; set; }
    public string PublisherName { get; set; }


}