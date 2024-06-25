using Application.Features.Books.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Books.Queries.GetBookByCategoryId;
public class GetListBookByCategoryIdQuery : IRequest<GetListResponse<GetListBookListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public Guid CategoryId { get; set; }
    public bool BypassCache { get; }
    public string? CacheKey => $"GetListBooks({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetBooks";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListBookByCategoryIdQueryHandler : IRequestHandler<GetListBookByCategoryIdQuery, GetListResponse<GetListBookListItemDto>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetListBookByCategoryIdQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<GetListResponse<GetListBookListItemDto>> Handle(
            GetListBookByCategoryIdQuery request,
            CancellationToken cancellationToken)
        {
            IPaginate<Book> books = await _bookRepository.GetListAsync(
                predicate: x => x.CategoryId == request.CategoryId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: p => p.Include(p => p.CategoryBooks)
            );
            GetListResponse<GetListBookListItemDto> response = _mapper.Map<GetListResponse<GetListBookListItemDto>>(books);
            return response;
        }

    }
}