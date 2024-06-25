using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.AuthorBooks;

public interface IAuthorBookService
{
    Task<AuthorBook?> GetAsync(
        Expression<Func<AuthorBook, bool>> predicate,
        Func<IQueryable<AuthorBook>, IIncludableQueryable<AuthorBook, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<AuthorBook>?> GetListAsync(
        Expression<Func<AuthorBook, bool>>? predicate = null,
        Func<IQueryable<AuthorBook>, IOrderedQueryable<AuthorBook>>? orderBy = null,
        Func<IQueryable<AuthorBook>, IIncludableQueryable<AuthorBook, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<AuthorBook> AddAsync(AuthorBook authorBook);
    Task<AuthorBook> UpdateAsync(AuthorBook authorBook);
    Task<AuthorBook> DeleteAsync(AuthorBook authorBook, bool permanent = false);
}
