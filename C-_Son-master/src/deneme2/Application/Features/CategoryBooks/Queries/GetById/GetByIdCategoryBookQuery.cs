using Application.Features.CategoryBooks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CategoryBooks.Queries.GetById;

public class GetByIdCategoryBookQuery : IRequest<GetByIdCategoryBookResponse>
{
    public Guid Id { get; set; }

    public class GetByIdCategoryBookQueryHandler : IRequestHandler<GetByIdCategoryBookQuery, GetByIdCategoryBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryBookRepository _categoryBookRepository;
        private readonly CategoryBookBusinessRules _categoryBookBusinessRules;

        public GetByIdCategoryBookQueryHandler(IMapper mapper, ICategoryBookRepository categoryBookRepository, CategoryBookBusinessRules categoryBookBusinessRules)
        {
            _mapper = mapper;
            _categoryBookRepository = categoryBookRepository;
            _categoryBookBusinessRules = categoryBookBusinessRules;
        }

        public async Task<GetByIdCategoryBookResponse> Handle(GetByIdCategoryBookQuery request, CancellationToken cancellationToken)
        {
            CategoryBook? categoryBook = await _categoryBookRepository.GetAsync(predicate: cb => cb.Id == request.Id, cancellationToken: cancellationToken);
            await _categoryBookBusinessRules.CategoryBookShouldExistWhenSelected(categoryBook);

            GetByIdCategoryBookResponse response = _mapper.Map<GetByIdCategoryBookResponse>(categoryBook);
            return response;
        }
    }
}