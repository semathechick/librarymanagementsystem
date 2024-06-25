using Application.Features.Rezervations.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Rezervations;

public class RezervationManager : IRezervationService
{
    private readonly IRezervationRepository _rezervationRepository;
    private readonly RezervationBusinessRules _rezervationBusinessRules;

    public RezervationManager(IRezervationRepository rezervationRepository, RezervationBusinessRules rezervationBusinessRules)
    {
        _rezervationRepository = rezervationRepository;
        _rezervationBusinessRules = rezervationBusinessRules;
    }

    public async Task<Rezervation?> GetAsync(
        Expression<Func<Rezervation, bool>> predicate,
        Func<IQueryable<Rezervation>, IIncludableQueryable<Rezervation, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Rezervation? rezervation = await _rezervationRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return rezervation;
    }

    public async Task<IPaginate<Rezervation>?> GetListAsync(
        Expression<Func<Rezervation, bool>>? predicate = null,
        Func<IQueryable<Rezervation>, IOrderedQueryable<Rezervation>>? orderBy = null,
        Func<IQueryable<Rezervation>, IIncludableQueryable<Rezervation, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Rezervation> rezervationList = await _rezervationRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return rezervationList;
    }

    public async Task<Rezervation> AddAsync(Rezervation rezervation)
    {
        Rezervation addedRezervation = await _rezervationRepository.AddAsync(rezervation);

        return addedRezervation;
    }

    public async Task<Rezervation> UpdateAsync(Rezervation rezervation)
    {
        Rezervation updatedRezervation = await _rezervationRepository.UpdateAsync(rezervation);

        return updatedRezervation;
    }

    public async Task<Rezervation> DeleteAsync(Rezervation rezervation, bool permanent = false)
    {
        Rezervation deletedRezervation = await _rezervationRepository.DeleteAsync(rezervation);

        return deletedRezervation;
    }
}
