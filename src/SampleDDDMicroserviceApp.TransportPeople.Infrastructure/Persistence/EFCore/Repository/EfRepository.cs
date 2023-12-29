using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Entity;

namespace SampleDDDMicroserviceApp.TransportPeople.Infrastructure.Persistence.EFCore.Repository;

internal class EfRepository<TEntity>(
    AppDbContext dbContext,
    IMapper mapper
    ) : ReadOnlyEfRepository<TEntity>(dbContext, mapper), IDisposable, IRepository<TEntity> where TEntity : class, IAggregateRoot
{
    public async Task AddAsync(TEntity itemToAdd, CancellationToken cancellationToken)
    {
        await DbSet.AddAsync(itemToAdd, cancellationToken);
    }

    public void Add(TEntity itemToAdd)
    {
        DbSet.Add(itemToAdd);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entitiesToAdd, CancellationToken cancellationToken)
    {
        await DbSet.AddRangeAsync(entitiesToAdd, cancellationToken);
    }

    public void AddRange(IEnumerable<TEntity> entitiesToAdd)
    {
        DbSet.AddRange(entitiesToAdd);
    }

    public void Update(TEntity itemToUpdate)
    {
        DbSet.Update(itemToUpdate);
    }

    public void Delete(TEntity itemToDelete)
    {
        if (itemToDelete is ArchivableEntity archivableEntity)
        {
            archivableEntity.SetArchived();
            Update(itemToDelete);
            DbSet.Entry(itemToDelete).State = EntityState.Detached;
        }
        else
        {
            DbSet.Remove(itemToDelete);
        }
    }
}