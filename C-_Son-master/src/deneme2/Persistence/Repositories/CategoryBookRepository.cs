using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CategoryBookRepository : EfRepositoryBase<CategoryBook, Guid, BaseDbContext>, ICategoryBookRepository
{
    public CategoryBookRepository(BaseDbContext context) : base(context)
    {
    }
}