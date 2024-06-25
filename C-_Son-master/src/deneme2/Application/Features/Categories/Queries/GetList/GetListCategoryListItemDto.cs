using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Categories.Queries.GetList;

public class GetListCategoryListItemDto : IDto
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; }
}