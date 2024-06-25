using NArchitecture.Core.Application.Responses;

namespace Application.Features.CategoryBooks.Queries.GetById;

public class GetByIdCategoryBookResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BookdId { get; set; }
    public Guid CategoryId { get; set; }
}