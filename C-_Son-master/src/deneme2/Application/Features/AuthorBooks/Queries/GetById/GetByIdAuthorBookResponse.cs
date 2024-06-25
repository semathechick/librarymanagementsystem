using NArchitecture.Core.Application.Responses;

namespace Application.Features.AuthorBooks.Queries.GetById;

public class GetByIdAuthorBookResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public Guid BookId { get; set; }
}