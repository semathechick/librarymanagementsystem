using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IBookPublisherRepository : IAsyncRepository<BookPublisher, Guid>, IRepository<BookPublisher, Guid>
{
}