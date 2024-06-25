using Application.Features.AuthorBooks.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.AuthorBooks;

public class AuthorBookManager : IAuthorBookService
{
    private readonly IAuthorBookRepository _authorBookRepository;
    private readonly AuthorBookBusinessRules _authorBookBusinessRules;

    public AuthorBookManager(IAuthorBookRepository authorBookRepository, AuthorBookBusinessRules authorBookBusinessRules)
    {
        _authorBookRepository = authorBookRepository;
        _authorBookBusinessRules = authorBookBusinessRules;
    }

    public async Task<AuthorBook?> GetAsync(
        Expression<Func<AuthorBook, bool>> predicate,
        Func<IQueryable<AuthorBook>, IIncludableQueryable<AuthorBook, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        AuthorBook? authorBook = await _authorBookRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return authorBook;
    }

    public async Task<IPaginate<AuthorBook>?> GetListAsync(
        Expression<Func<AuthorBook, bool>>? predicate = null,
        Func<IQueryable<AuthorBook>, IOrderedQueryable<AuthorBook>>? orderBy = null,
        Func<IQueryable<AuthorBook>, IIncludableQueryable<AuthorBook, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<AuthorBook> authorBookList = await _authorBookRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return authorBookList;
    }

    public async Task<AuthorBook> AddAsync(AuthorBook authorBook)
    {
        AuthorBook addedAuthorBook = await _authorBookRepository.AddAsync(authorBook);

        return addedAuthorBook;
    }

    public async Task<AuthorBook> UpdateAsync(AuthorBook authorBook)
    {
        AuthorBook updatedAuthorBook = await _authorBookRepository.UpdateAsync(authorBook);

        return updatedAuthorBook;
    }

    public async Task<AuthorBook> DeleteAsync(AuthorBook authorBook, bool permanent = false)
    {
        AuthorBook deletedAuthorBook = await _authorBookRepository.DeleteAsync(authorBook);

        return deletedAuthorBook;
    }
}
