using NArchitecture.Core.Application.Responses;

namespace Application.Features.Authors.Queries.GetById;

public class GetByIdAuthorResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string IdentityNumber { get; set; }
}