using NArchitecture.Core.Application.Responses;

namespace Application.Features.CategoryBooks.Commands.Delete;

public class DeletedCategoryBookResponse : IResponse
{
    public Guid Id { get; set; }
}