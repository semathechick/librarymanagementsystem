using Application.Features.CategoryBooks.Constants;
using Application.Features.CategoryBooks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.CategoryBooks.Commands.Delete;

public class DeleteCategoryBookCommand : IRequest<DeletedCategoryBookResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCategoryBooks"];

    public class DeleteCategoryBookCommandHandler : IRequestHandler<DeleteCategoryBookCommand, DeletedCategoryBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryBookRepository _categoryBookRepository;
        private readonly CategoryBookBusinessRules _categoryBookBusinessRules;

        public DeleteCategoryBookCommandHandler(IMapper mapper, ICategoryBookRepository categoryBookRepository,
                                         CategoryBookBusinessRules categoryBookBusinessRules)
        {
            _mapper = mapper;
            _categoryBookRepository = categoryBookRepository;
            _categoryBookBusinessRules = categoryBookBusinessRules;
        }

        public async Task<DeletedCategoryBookResponse> Handle(DeleteCategoryBookCommand request, CancellationToken cancellationToken)
        {
            CategoryBook? categoryBook = await _categoryBookRepository.GetAsync(predicate: cb => cb.Id == request.Id, cancellationToken: cancellationToken);
            await _categoryBookBusinessRules.CategoryBookShouldExistWhenSelected(categoryBook);

            await _categoryBookRepository.DeleteAsync(categoryBook!);

            DeletedCategoryBookResponse response = _mapper.Map<DeletedCategoryBookResponse>(categoryBook);
            return response;
        }
    }
}