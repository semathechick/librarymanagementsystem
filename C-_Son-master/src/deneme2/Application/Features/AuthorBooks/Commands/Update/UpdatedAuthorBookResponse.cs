using NArchitecture.Core.Application.Responses;

namespace Application.Features.AuthorBooks.Commands.Update;

public class UpdatedAuthorBookResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public Guid BookId { get; set; }
}