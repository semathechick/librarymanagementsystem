using NArchitecture.Core.Application.Responses;

namespace Application.Features.Rezervations.Commands.Delete;

public class DeletedRezervationResponse : IResponse
{
    public Guid Id { get; set; }
}