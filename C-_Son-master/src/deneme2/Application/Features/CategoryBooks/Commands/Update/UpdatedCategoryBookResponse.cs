using NArchitecture.Core.Application.Responses;

namespace Application.Features.CategoryBooks.Commands.Update;

public class UpdatedCategoryBookResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BookdId { get; set; }
    public Guid CategoryId { get; set; }
}