using NArchitecture.Core.Application.Responses;

namespace Application.Features.AuthorBooks.Commands.Delete;

public class DeletedAuthorBookResponse : IResponse
{
    public Guid Id { get; set; }
}