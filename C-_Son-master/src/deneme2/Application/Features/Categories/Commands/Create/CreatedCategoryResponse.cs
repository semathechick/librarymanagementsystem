using NArchitecture.Core.Application.Responses;

namespace Application.Features.Categories.Commands.Create;

public class CreatedCategoryResponse : IResponse
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; }
}