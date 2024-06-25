using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Rezervations;

public interface IRezervationService
{
    Task<Rezervation?> GetAsync(
        Expression<Func<Rezervation, bool>> predicate,
        Func<IQueryable<Rezervation>, IIncludableQueryable<Rezervation, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Rezervation>?> GetListAsync(
        Expression<Func<Rezervation, bool>>? predicate = null,
        Func<IQueryable<Rezervation>, IOrderedQueryable<Rezervation>>? orderBy = null,
        Func<IQueryable<Rezervation>, IIncludableQueryable<Rezervation, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Rezervation> AddAsync(Rezervation rezervation);
    Task<Rezervation> UpdateAsync(Rezervation rezervation);
    Task<Rezervation> DeleteAsync(Rezervation rezervation, bool permanent = false);
}
