using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Rezervations.Queries.GetList;

public class GetListRezervationListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public string BookName { get; set; }
    public Guid MemberId { get; set; }
    public DateTime RezervationDate { get; set; }
    public DateTime ExpirationDate { get; set; }
}