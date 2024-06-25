using Application.Features.Authors.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.Authors.Commands.Create;

public class CreateAuthorCommand : IRequest<CreatedAuthorResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }
    public string IdentityNumber { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetAuthors"];

    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, CreatedAuthorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;
        private readonly AuthorBusinessRules _authorBusinessRules;

        public CreateAuthorCommandHandler(IMapper mapper, IAuthorRepository authorRepository,
                                         AuthorBusinessRules authorBusinessRules)
        {
            _mapper = mapper;
            _authorRepository = authorRepository;
            _authorBusinessRules = authorBusinessRules;
        }

        public async Task<CreatedAuthorResponse> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            await _authorBusinessRules.AuthorShouldBeNotExists(request.Name);
            Author author = _mapper.Map<Author>(request);

            await _authorRepository.AddAsync(author);

            CreatedAuthorResponse response = _mapper.Map<CreatedAuthorResponse>(author);
            return response;
        }
    }
}