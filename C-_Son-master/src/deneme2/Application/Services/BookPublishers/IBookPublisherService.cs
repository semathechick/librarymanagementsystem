using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.BookPublishers;

public interface IBookPublisherService
{
    Task<BookPublisher?> GetAsync(
        Expression<Func<BookPublisher, bool>> predicate,
        Func<IQueryable<BookPublisher>, IIncludableQueryable<BookPublisher, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<BookPublisher>?> GetListAsync(
        Expression<Func<BookPublisher, bool>>? predicate = null,
        Func<IQueryable<BookPublisher>, IOrderedQueryable<BookPublisher>>? orderBy = null,
        Func<IQueryable<BookPublisher>, IIncludableQueryable<BookPublisher, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<BookPublisher> AddAsync(BookPublisher bookPublisher);
    Task<BookPublisher> UpdateAsync(BookPublisher bookPublisher);
    Task<BookPublisher> DeleteAsync(BookPublisher bookPublisher, bool permanent = false);
}
