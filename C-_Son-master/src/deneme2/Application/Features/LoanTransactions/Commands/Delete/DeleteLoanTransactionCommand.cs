using Application.Features.LoanTransactions.Constants;
using Application.Features.LoanTransactions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.LoanTransactions.Commands.Delete;

public class DeleteLoanTransactionCommand : IRequest<DeletedLoanTransactionResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetLoanTransactions"];

    public class DeleteLoanTransactionCommandHandler : IRequestHandler<DeleteLoanTransactionCommand, DeletedLoanTransactionResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILoanTransactionRepository _loanTransactionRepository;
        private readonly LoanTransactionBusinessRules _loanTransactionBusinessRules;

        public DeleteLoanTransactionCommandHandler(IMapper mapper, ILoanTransactionRepository loanTransactionRepository,
                                         LoanTransactionBusinessRules loanTransactionBusinessRules)
        {
            _mapper = mapper;
            _loanTransactionRepository = loanTransactionRepository;
            _loanTransactionBusinessRules = loanTransactionBusinessRules;
        }

        public async Task<DeletedLoanTransactionResponse> Handle(DeleteLoanTransactionCommand request, CancellationToken cancellationToken)
        {
            LoanTransaction? loanTransaction = await _loanTransactionRepository.GetAsync(predicate: lt => lt.Id == request.Id, cancellationToken: cancellationToken);
            await _loanTransactionBusinessRules.LoanTransactionShouldExistWhenSelected(loanTransaction);

            await _loanTransactionRepository.DeleteAsync(loanTransaction!,true);

            DeletedLoanTransactionResponse response = _mapper.Map<DeletedLoanTransactionResponse>(loanTransaction);
            return response;
        }
    }
}