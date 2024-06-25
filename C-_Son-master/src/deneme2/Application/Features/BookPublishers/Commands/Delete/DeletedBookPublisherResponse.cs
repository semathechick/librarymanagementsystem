using NArchitecture.Core.Application.Responses;

namespace Application.Features.BookPublishers.Commands.Delete;

public class DeletedBookPublisherResponse : IResponse
{
    public Guid Id { get; set; }
}