using Application.Features.LoanTransactions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using Domain.Enums;

namespace Application.Features.LoanTransactions.Commands.Create;

public class CreateLoanTransactionCommand : IRequest<CreatedLoanTransactionResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid MemberId { get; set; }
    public Guid BookId { get; set; }
    public ReturnStatus ReturnStatus { get; set; }
    public DateTime ReturnTime { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetLoanTransactions"];

    public class CreateLoanTransactionCommandHandler : IRequestHandler<CreateLoanTransactionCommand, CreatedLoanTransactionResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILoanTransactionRepository _loanTransactionRepository;
        private readonly LoanTransactionBusinessRules _loanTransactionBusinessRules;

        public CreateLoanTransactionCommandHandler(IMapper mapper, ILoanTransactionRepository loanTransactionRepository,
                                         LoanTransactionBusinessRules loanTransactionBusinessRules)
        {
            _mapper = mapper;
            _loanTransactionRepository = loanTransactionRepository;
            _loanTransactionBusinessRules = loanTransactionBusinessRules;
        }

        public async Task<CreatedLoanTransactionResponse> Handle(CreateLoanTransactionCommand request, CancellationToken cancellationToken)
        {
            await _loanTransactionBusinessRules.CheckIfBookPreviouslyBorrowed(request.BookId);
            LoanTransaction loanTransaction = _mapper.Map<LoanTransaction>(request);
            loanTransaction.ReturnTime = loanTransaction.ReturnTime.Date.Add(new TimeSpan(17, 0, 0));
            await _loanTransactionRepository.AddAsync(loanTransaction);

            CreatedLoanTransactionResponse response = _mapper.Map<CreatedLoanTransactionResponse>(loanTransaction);
            return response;
        }
    }
}