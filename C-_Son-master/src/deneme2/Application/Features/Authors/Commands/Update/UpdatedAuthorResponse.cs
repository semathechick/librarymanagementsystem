using NArchitecture.Core.Application.Responses;

namespace Application.Features.Authors.Commands.Update;

public class UpdatedAuthorResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string IdentityNumber { get; set; }
}