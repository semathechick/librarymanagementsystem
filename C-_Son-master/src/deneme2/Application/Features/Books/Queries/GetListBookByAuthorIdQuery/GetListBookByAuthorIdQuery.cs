
using Application.Features.Books.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;



namespace Application.Features.Books.Queries.GetBookByAuthorId;
public class GetListBookByAuthorIdQuery : IRequest<GetListResponse<GetListBookListItemDto>>
{

    public PageRequest PageRequest { get; set; }
    public Guid AuthorId { get; set; }
    public bool BypassCache { get; }
    public string? CacheKey => $"GetListBooks({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetBooks";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListBookByAuthorIdQueryHandler : IRequestHandler<GetListBookByAuthorIdQuery, GetListResponse<GetListBookListItemDto>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetListBookByAuthorIdQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<GetListResponse<GetListBookListItemDto>> Handle(
            GetListBookByAuthorIdQuery request,
            CancellationToken cancellationToken)
        {
            IPaginate<Book> books = await _bookRepository.GetListAsync(
                predicate: x => x.AuthorId == request.AuthorId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: p => p.Include(p => p.AuthorBooks)
            );
            GetListResponse<GetListBookListItemDto> response = _mapper.Map<GetListResponse<GetListBookListItemDto>>(books);
            return response;
        }

    }
}