using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Entity;

namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Contracts;

public interface IReadOnlyRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity?> GetByIdAsync<TId>(IStronglyTypedId<TId> id, CancellationToken cancellationToken) where TId : unmanaged;

    Task<List<TEntity>> ToListAsync(Specification<TEntity> specification, CancellationToken cancellationToken);

    Task<List<TEntity>> ToListAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Projection using AutoMapper .ProjectTo extension method
    /// </summary>
    Task<List<TMapperDestination>> ToListProjectedAsync<TMapperDestination>(CancellationToken cancellationToken);

    /// <summary>
    /// Projection using AutoMapper .ProjectTo extension method along using .Distinct method
    /// </summary>
    Task<List<TMapperDestination>> ToListProjectedDistinctAsync<TMapperDestination>(CancellationToken cancellationToken);

    /// <summary>
    /// Projection using AutoMapper .ProjectTo extension method
    /// </summary>
    Task<List<TMapperDestination>> ToListProjectedAsync<TMapperDestination>(Specification<TEntity> specification, CancellationToken cancellationToken);

    /// <summary>
    /// Projection using AutoMapper .ProjectTo extension method along using .Distinct method
    /// </summary>
    Task<List<TMapperDestination>> ToListProjectedDistinctAsync<TMapperDestination>(Specification<TEntity> specification, CancellationToken cancellationToken);

    /// <summary>
    /// Projection using Specification output
    /// </summary>
    Task<List<TSpecResult>> ToListProjectedAsync<TSpecResult>(Specification<TEntity, TSpecResult> specification, CancellationToken cancellationToken);

    /// <summary>
    /// Projection using Specification output along using .Distinct method
    /// </summary>
    Task<List<TSpecResult>> ToListProjectedDistinctAsync<TSpecResult>(Specification<TEntity, TSpecResult> specification, CancellationToken cancellationToken);

    Task<bool> AnyAsync(Specification<TEntity> specification, CancellationToken cancellationToken);

    Task<TEntity?> FirstOrDefaultAsync(Specification<TEntity> specification, CancellationToken cancellationToken);

    /// <summary>
    /// Projection using Specification output
    /// </summary>
    Task<TSpecResult?> FirstOrDefaultProjectedAsync<TSpecResult>(Specification<TEntity, TSpecResult> specification, CancellationToken cancellationToken);

    /// <summary>
    /// Projection using AutoMapper .ProjectTo extension method
    /// </summary>
    Task<TMapperDestination?> FirstOrDefaultProjectedAsync<TMapperDestination>(Specification<TEntity> specification, CancellationToken cancellationToken);

    Task<TEntity?> SingleOrDefaultAsync(Specification<TEntity> specification, CancellationToken cancellationToken);

    Task<TEntity> FirstAsync(Specification<TEntity> specification, CancellationToken cancellationToken);

    IQueryable<TEntity> AsQueryable();

    IQueryable<TEntity> AsQueryable(Specification<TEntity> specification);
}