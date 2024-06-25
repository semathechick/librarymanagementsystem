using NArchitecture.Core.Application.Dtos;

namespace Application.Features.AuthorBooks.Queries.GetList;

public class GetListAuthorBookListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public Guid BookId { get; set; }
}