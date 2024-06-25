using Application.Features.CategoryBooks.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.CategoryBooks;

public class CategoryBookManager : ICategoryBookService
{
    private readonly ICategoryBookRepository _categoryBookRepository;
    private readonly CategoryBookBusinessRules _categoryBookBusinessRules;

    public CategoryBookManager(ICategoryBookRepository categoryBookRepository, CategoryBookBusinessRules categoryBookBusinessRules)
    {
        _categoryBookRepository = categoryBookRepository;
        _categoryBookBusinessRules = categoryBookBusinessRules;
    }

    public async Task<CategoryBook?> GetAsync(
        Expression<Func<CategoryBook, bool>> predicate,
        Func<IQueryable<CategoryBook>, IIncludableQueryable<CategoryBook, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        CategoryBook? categoryBook = await _categoryBookRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return categoryBook;
    }

    public async Task<IPaginate<CategoryBook>?> GetListAsync(
        Expression<Func<CategoryBook, bool>>? predicate = null,
        Func<IQueryable<CategoryBook>, IOrderedQueryable<CategoryBook>>? orderBy = null,
        Func<IQueryable<CategoryBook>, IIncludableQueryable<CategoryBook, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<CategoryBook> categoryBookList = await _categoryBookRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return categoryBookList;
    }

    public async Task<CategoryBook> AddAsync(CategoryBook categoryBook)
    {
        CategoryBook addedCategoryBook = await _categoryBookRepository.AddAsync(categoryBook);

        return addedCategoryBook;
    }

    public async Task<CategoryBook> UpdateAsync(CategoryBook categoryBook)
    {
        CategoryBook updatedCategoryBook = await _categoryBookRepository.UpdateAsync(categoryBook);

        return updatedCategoryBook;
    }

    public async Task<CategoryBook> DeleteAsync(CategoryBook categoryBook, bool permanent = false)
    {
        CategoryBook deletedCategoryBook = await _categoryBookRepository.DeleteAsync(categoryBook);

        return deletedCategoryBook;
    }
}
