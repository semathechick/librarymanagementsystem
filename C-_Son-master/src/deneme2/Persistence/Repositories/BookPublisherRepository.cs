using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class BookPublisherRepository : EfRepositoryBase<BookPublisher, Guid, BaseDbContext>, IBookPublisherRepository
{
    public BookPublisherRepository(BaseDbContext context) : base(context)
    {
    }
}