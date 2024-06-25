using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class AuthorBookRepository : EfRepositoryBase<AuthorBook, Guid, BaseDbContext>, IAuthorBookRepository
{
    public AuthorBookRepository(BaseDbContext context) : base(context)
    {
    }
}