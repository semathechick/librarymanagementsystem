using Application.Features.LoanTransactions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.LoanTransactions.Queries.GetById;

public class GetByIdLoanTransactionQuery : IRequest<GetByIdLoanTransactionResponse>
{
    public Guid Id { get; set; }

    public class GetByIdLoanTransactionQueryHandler : IRequestHandler<GetByIdLoanTransactionQuery, GetByIdLoanTransactionResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILoanTransactionRepository _loanTransactionRepository;
        private readonly LoanTransactionBusinessRules _loanTransactionBusinessRules;

        public GetByIdLoanTransactionQueryHandler(IMapper mapper, ILoanTransactionRepository loanTransactionRepository, LoanTransactionBusinessRules loanTransactionBusinessRules)
        {
            _mapper = mapper;
            _loanTransactionRepository = loanTransactionRepository;
            _loanTransactionBusinessRules = loanTransactionBusinessRules;
        }

        public async Task<GetByIdLoanTransactionResponse> Handle(GetByIdLoanTransactionQuery request, CancellationToken cancellationToken)
        {
            LoanTransaction? loanTransaction = await _loanTransactionRepository.GetAsync(predicate: lt => lt.Id == request.Id, cancellationToken: cancellationToken);
            await _loanTransactionBusinessRules.LoanTransactionShouldExistWhenSelected(loanTransaction);

            GetByIdLoanTransactionResponse response = _mapper.Map<GetByIdLoanTransactionResponse>(loanTransaction);
            return response;
        }
    }
}