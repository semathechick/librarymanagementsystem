using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LoanTransactions.Queries.GetList;

public class GetListLoanTransactionQuery : IRequest<GetListResponse<GetListLoanTransactionListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListLoanTransactions({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetLoanTransactions";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListLoanTransactionQueryHandler : IRequestHandler<GetListLoanTransactionQuery, GetListResponse<GetListLoanTransactionListItemDto>>
    {
        private readonly ILoanTransactionRepository _loanTransactionRepository;
        private readonly IMapper _mapper;

        public GetListLoanTransactionQueryHandler(ILoanTransactionRepository loanTransactionRepository, IMapper mapper)
        {
            _loanTransactionRepository = loanTransactionRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListLoanTransactionListItemDto>> Handle(GetListLoanTransactionQuery request, CancellationToken cancellationToken)
        {
            IPaginate<LoanTransaction> loanTransactions = await _loanTransactionRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, include:i=>i.Include(i=>i.Book), 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListLoanTransactionListItemDto> response = _mapper.Map<GetListResponse<GetListLoanTransactionListItemDto>>(loanTransactions);
            return response;
        }
    }
}