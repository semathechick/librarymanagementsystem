using NArchitecture.Core.Application.Responses;

namespace Application.Features.AuthorBooks.Commands.Create;

public class CreatedAuthorBookResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public Guid BookId { get; set; }
}