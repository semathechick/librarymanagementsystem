using Application.Features.BookPublishers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.BookPublishers.Queries.GetById;

public class GetByIdBookPublisherQuery : IRequest<GetByIdBookPublisherResponse>
{
    public Guid Id { get; set; }

    public class GetByIdBookPublisherQueryHandler : IRequestHandler<GetByIdBookPublisherQuery, GetByIdBookPublisherResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBookPublisherRepository _bookPublisherRepository;
        private readonly BookPublisherBusinessRules _bookPublisherBusinessRules;

        public GetByIdBookPublisherQueryHandler(IMapper mapper, IBookPublisherRepository bookPublisherRepository, BookPublisherBusinessRules bookPublisherBusinessRules)
        {
            _mapper = mapper;
            _bookPublisherRepository = bookPublisherRepository;
            _bookPublisherBusinessRules = bookPublisherBusinessRules;
        }

        public async Task<GetByIdBookPublisherResponse> Handle(GetByIdBookPublisherQuery request, CancellationToken cancellationToken)
        {
            BookPublisher? bookPublisher = await _bookPublisherRepository.GetAsync(predicate: bp => bp.Id == request.Id, cancellationToken: cancellationToken);
            await _bookPublisherBusinessRules.BookPublisherShouldExistWhenSelected(bookPublisher);

            GetByIdBookPublisherResponse response = _mapper.Map<GetByIdBookPublisherResponse>(bookPublisher);
            return response;
        }
    }
}