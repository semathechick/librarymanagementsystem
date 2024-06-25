using Application.Features.Books.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Books;

public class BookManager : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly BookBusinessRules _bookBusinessRules;

    public BookManager(IBookRepository bookRepository, BookBusinessRules bookBusinessRules)
    {
        _bookRepository = bookRepository;
        _bookBusinessRules = bookBusinessRules;
    }

    public async Task<Book?> GetAsync(
        Expression<Func<Book, bool>> predicate,
        Func<IQueryable<Book>, IIncludableQueryable<Book, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Book? book = await _bookRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return book;
    }

    public async Task<IPaginate<Book>?> GetListAsync(
        Expression<Func<Book, bool>>? predicate = null,
        Func<IQueryable<Book>, IOrderedQueryable<Book>>? orderBy = null,
        Func<IQueryable<Book>, IIncludableQueryable<Book, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Book> bookList = await _bookRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return bookList;
    }

    public async Task<Book> AddAsync(Book book)
    {
        Book addedBook = await _bookRepository.AddAsync(book);

        return addedBook;
    }

    public async Task<Book> UpdateAsync(Book book)
    {
        Book updatedBook = await _bookRepository.UpdateAsync(book);

        return updatedBook;
    }

    public async Task<Book> DeleteAsync(Book book, bool permanent = false)
    {
        Book deletedBook = await _bookRepository.DeleteAsync(book);

        return deletedBook;
    }
}
