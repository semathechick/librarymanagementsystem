using Application.Features.LoanTransactions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using Domain.Enums;

namespace Application.Features.LoanTransactions.Commands.Update;

public class UpdateLoanTransactionCommand : IRequest<UpdatedLoanTransactionResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public Guid BookId { get; set; }
    public ReturnStatus ReturnStatus { get; set; }
    

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetLoanTransactions"];

    public class UpdateLoanTransactionCommandHandler : IRequestHandler<UpdateLoanTransactionCommand, UpdatedLoanTransactionResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILoanTransactionRepository _loanTransactionRepository;
        private readonly LoanTransactionBusinessRules _loanTransactionBusinessRules;

        public UpdateLoanTransactionCommandHandler(IMapper mapper, ILoanTransactionRepository loanTransactionRepository,
                                         LoanTransactionBusinessRules loanTransactionBusinessRules)
        {
            _mapper = mapper;
            _loanTransactionRepository = loanTransactionRepository;
            _loanTransactionBusinessRules = loanTransactionBusinessRules;
        }

        public async Task<UpdatedLoanTransactionResponse> Handle(UpdateLoanTransactionCommand request, CancellationToken cancellationToken)
        {
            LoanTransaction? loanTransaction = await _loanTransactionRepository.GetAsync(predicate: lt => lt.Id == request.Id, cancellationToken: cancellationToken);
            await _loanTransactionBusinessRules.LoanTransactionShouldExistWhenSelected(loanTransaction);
            loanTransaction = _mapper.Map(request, loanTransaction);

            await _loanTransactionRepository.UpdateAsync(loanTransaction!);
            await _loanTransactionRepository.DeleteAsync(loanTransaction!,true);
            UpdatedLoanTransactionResponse response = _mapper.Map<UpdatedLoanTransactionResponse>(loanTransaction);
            return response;
        }
    }
}