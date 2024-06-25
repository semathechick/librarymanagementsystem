using NArchitecture.Core.Application.Dtos;

namespace Application.Features.BookPublishers.Queries.GetList;

public class GetListBookPublisherListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid PublisherId { get; set; }
}