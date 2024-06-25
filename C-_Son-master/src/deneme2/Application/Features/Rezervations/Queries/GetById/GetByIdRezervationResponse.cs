using NArchitecture.Core.Application.Responses;

namespace Application.Features.Rezervations.Queries.GetById;

public class GetByIdRezervationResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid MemberId { get; set; }
    public DateTime RezervationDate { get; set; }
    public DateTime ExpirationDate { get; set; }
}