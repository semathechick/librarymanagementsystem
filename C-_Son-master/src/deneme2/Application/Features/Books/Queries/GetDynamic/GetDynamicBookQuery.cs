using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Books.Queries.GetDynamic;
public class GetDynamicBookQuery : IRequest<GetListResponse<GetDynamicBookItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery Dynamic { get; set; }

    public class GetDynamicBookQueryHandler : IRequestHandler<GetDynamicBookQuery, GetListResponse<GetDynamicBookItemDto>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetDynamicBookQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<GetListResponse<GetDynamicBookItemDto>> Handle(GetDynamicBookQuery request, CancellationToken cancellationToken)
        {
            var books = await
                _bookRepository.GetListByDynamicAsync(request.Dynamic, index: request.PageRequest.PageIndex, size: request.PageRequest.PageSize, include: i => i.Include(i => i.CategoryBooks));
            GetListResponse<GetDynamicBookItemDto> response = _mapper.Map<GetListResponse<GetDynamicBookItemDto>>(books);
            return response;
        }


    }
}