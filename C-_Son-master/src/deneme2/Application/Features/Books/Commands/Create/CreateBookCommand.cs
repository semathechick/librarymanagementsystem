using Application.Features.Books.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using Application.Services.Categories;
using Application.Services.CategoryBooks;
using Application.Services.BookPublishers;
using NArchitecture.Core.Application.Pipelines.Authorization;
using Application.Features.Users.Constants;
using System.ComponentModel;
using Application.Features.Books.Constants;
using Application.Services.AuthorBooks;

namespace Application.Features.Books.Commands.Create;

public class CreateBookCommand : IRequest<CreatedBookResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }
    public string ISBN { get; set; }
    public Guid CategoryId { get; set; }
    public Guid PublisherId { get; set; }
    public Guid AuthorId { get; set; }
    public int Page { get; set; }
    public string Language { get; set; }
    public int UnitsInStock { get; set; }
    public string Description { get; set; }


    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBooks"];

    public string[] Roles => new[] { BooksOperationClaims.Create ,BooksOperationClaims.Admin};

    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, CreatedBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        private readonly BookBusinessRules _bookBusinessRules;
        private readonly ICategoryBookService _categoryBookService;
        private readonly IBookPublisherService _bookPublisherService;
        private readonly IAuthorBookService _authorBookService;


        public CreateBookCommandHandler(IMapper mapper, IBookRepository bookRepository,
                                         BookBusinessRules bookBusinessRules, ICategoryBookService categoryBookService, IBookPublisherService bookPublisherService, IAuthorBookService authorBookService)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
            _bookBusinessRules = bookBusinessRules;
            _categoryBookService = categoryBookService;
            _bookPublisherService = bookPublisherService;
            _authorBookService = authorBookService;
        }

        public async Task<CreatedBookResponse> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {

            await _bookBusinessRules.BookShouldBeNotExists(request.Name);
            Book book = _mapper.Map<Book>(request);
            
            await _bookRepository.AddAsync(book);
            
            

            CreatedBookResponse response = _mapper.Map<CreatedBookResponse>(book);

            CategoryBook categoryBook = await _categoryBookService.AddAsync(new CategoryBook()
            {
                CategoryId = book.CategoryId,
                BookId = response.Id
            }) ;

            BookPublisher bookPublisher = await _bookPublisherService.AddAsync(new BookPublisher()
            {
                PublisherId = book.PublisherId,
                BookId = response.Id
            });
            AuthorBook authorBook = await _authorBookService.AddAsync(new AuthorBook()
            {
                AuthorId=book.AuthorId,
                BookId=response.Id
            });
            return response;
            
            
        }
    }
}