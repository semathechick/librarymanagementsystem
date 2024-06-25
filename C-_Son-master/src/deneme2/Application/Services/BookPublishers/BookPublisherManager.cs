using Application.Features.BookPublishers.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.BookPublishers;

public class BookPublisherManager : IBookPublisherService
{
    private readonly IBookPublisherRepository _bookPublisherRepository;
    private readonly BookPublisherBusinessRules _bookPublisherBusinessRules;

    public BookPublisherManager(IBookPublisherRepository bookPublisherRepository, BookPublisherBusinessRules bookPublisherBusinessRules)
    {
        _bookPublisherRepository = bookPublisherRepository;
        _bookPublisherBusinessRules = bookPublisherBusinessRules;
    }

    public async Task<BookPublisher?> GetAsync(
        Expression<Func<BookPublisher, bool>> predicate,
        Func<IQueryable<BookPublisher>, IIncludableQueryable<BookPublisher, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        BookPublisher? bookPublisher = await _bookPublisherRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return bookPublisher;
    }

    public async Task<IPaginate<BookPublisher>?> GetListAsync(
        Expression<Func<BookPublisher, bool>>? predicate = null,
        Func<IQueryable<BookPublisher>, IOrderedQueryable<BookPublisher>>? orderBy = null,
        Func<IQueryable<BookPublisher>, IIncludableQueryable<BookPublisher, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<BookPublisher> bookPublisherList = await _bookPublisherRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return bookPublisherList;
    }

    public async Task<BookPublisher> AddAsync(BookPublisher bookPublisher)
    {
        BookPublisher addedBookPublisher = await _bookPublisherRepository.AddAsync(bookPublisher);

        return addedBookPublisher;
    }

    public async Task<BookPublisher> UpdateAsync(BookPublisher bookPublisher)
    {
        BookPublisher updatedBookPublisher = await _bookPublisherRepository.UpdateAsync(bookPublisher);

        return updatedBookPublisher;
    }

    public async Task<BookPublisher> DeleteAsync(BookPublisher bookPublisher, bool permanent = false)
    {
        BookPublisher deletedBookPublisher = await _bookPublisherRepository.DeleteAsync(bookPublisher);

        return deletedBookPublisher;
    }
}
