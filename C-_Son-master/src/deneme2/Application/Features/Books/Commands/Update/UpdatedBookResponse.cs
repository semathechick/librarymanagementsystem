using NArchitecture.Core.Application.Responses;

namespace Application.Features.Books.Commands.Update;

public class UpdatedBookResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ISBN { get; set; }
}