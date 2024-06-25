using NArchitecture.Core.Application.Responses;

namespace Application.Features.Books.Commands.Delete;

public class DeletedBookResponse : IResponse
{
    public Guid Id { get; set; }
}