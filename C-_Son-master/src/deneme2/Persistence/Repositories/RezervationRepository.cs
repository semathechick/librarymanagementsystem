using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class RezervationRepository : EfRepositoryBase<Rezervation, Guid, BaseDbContext>, IRezervationRepository
{
    public RezervationRepository(BaseDbContext context) : base(context)
    {
    }
}