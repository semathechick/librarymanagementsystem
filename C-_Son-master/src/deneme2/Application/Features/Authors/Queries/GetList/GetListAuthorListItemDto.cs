using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Authors.Queries.GetList;

public class GetListAuthorListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string IdentityNumber { get; set; }
}