using Domain.Enums;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.LoanTransactions.Queries.GetList;

public class GetListLoanTransactionListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public string BookName { get; set; }
    public Guid BookId { get; set; }
    public ReturnStatus ReturnStatus { get; set; }
    public DateTime ReturnTime { get; set; }
}