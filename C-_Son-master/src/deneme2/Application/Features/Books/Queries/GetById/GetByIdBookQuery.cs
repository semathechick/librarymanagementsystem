using Application.Features.Books.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Books.Queries.GetById;

public class GetByIdBookQuery : IRequest<GetByIdBookResponse>
{
    public Guid Id { get; set; }

    public class GetByIdBookQueryHandler : IRequestHandler<GetByIdBookQuery, GetByIdBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        private readonly BookBusinessRules _bookBusinessRules;

        public GetByIdBookQueryHandler(IMapper mapper, IBookRepository bookRepository, BookBusinessRules bookBusinessRules)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
            _bookBusinessRules = bookBusinessRules;
        }

        public async Task<GetByIdBookResponse> Handle(GetByIdBookQuery request, CancellationToken cancellationToken)
        {
            Book? book = await _bookRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
            await _bookBusinessRules.BookShouldExistWhenSelected(book);

            GetByIdBookResponse response = _mapper.Map<GetByIdBookResponse>(book);
            return response;
        }
    }
}