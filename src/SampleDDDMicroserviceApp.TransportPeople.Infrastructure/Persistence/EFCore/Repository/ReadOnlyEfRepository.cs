﻿using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Entity;
using IEntity = SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Entity.IEntity;

namespace SampleDDDMicroserviceApp.TransportPeople.Infrastructure.Persistence.EFCore.Repository;

internal class ReadOnlyEfRepository<TEntity>(
    AppDbContext dbContext,
    IMapper mapper
    ) : IDisposable, IReadOnlyRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly DbSet<TEntity> DbSet = dbContext.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync<TKey>(IStronglyTypedId<TKey> id, CancellationToken cancellationToken) where TKey : unmanaged
    {
        return await DbSet.FindAsync(id.Value, cancellationToken);
    }

    public IQueryable<TEntity> AsQueryable()
    {
        return DbSet;
    }

    public IQueryable<TEntity> AsQueryable(Specification<TEntity> specification)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: DbSet,
            specification: specification
        );

        return queryResult;
    }

    public async Task<List<TEntity>> ToListAsync(Specification<TEntity> specification, CancellationToken cancellationToken)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: DbSet,
            specification: specification
        );

        return await queryResult.ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> ToListAsync(CancellationToken cancellationToken)
    {
        return await DbSet.ToListAsync(cancellationToken);
    }

    public async Task<List<TMapperDestination>> ToListAsync<TMapperDestination>(CancellationToken cancellationToken)
    {
        return await DbSet
            .ProjectTo<TMapperDestination>(mapper.ConfigurationProvider)
            .Distinct()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TMapperDestination>> ToListDistinctAsync<TMapperDestination>(CancellationToken cancellationToken)
    {
        return await DbSet
            .ProjectTo<TMapperDestination>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TMapperDestination>> ToListAsync<TMapperDestination>(Specification<TEntity> specification, CancellationToken cancellationToken)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: DbSet,
            specification: specification
        );

        return await queryResult
            .ProjectTo<TMapperDestination>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TMapperDestination>> ToListDistinctAsync<TMapperDestination>(Specification<TEntity> specification, CancellationToken cancellationToken)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: DbSet,
            specification: specification
        );

        return await queryResult
            .ProjectTo<TMapperDestination>(mapper.ConfigurationProvider)
            .Distinct()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TSpecResult>> ToListAsync<TSpecResult>(Specification<TEntity, TSpecResult> specification, CancellationToken cancellationToken)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: DbSet,
            specification: specification
        );

        return await queryResult
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TSpecResult>> ToListDistinctAsync<TSpecResult>(Specification<TEntity, TSpecResult> specification, CancellationToken cancellationToken)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: DbSet,
            specification: specification
        );

        return await queryResult
            .Distinct()
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(Specification<TEntity> specification, CancellationToken cancellationToken)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: DbSet,
            specification: specification
        );

        return await queryResult.AnyAsync(cancellationToken);
    }

    public async Task<TEntity?> FirstOrDefaultAsync(Specification<TEntity> specification, CancellationToken cancellationToken)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: DbSet,
            specification: specification
        );

        return await queryResult.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TOut?> FirstOrDefaultAsync<TOut>(Specification<TEntity, TOut> specification, CancellationToken cancellationToken)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: DbSet,
            specification: specification
        );

        return await queryResult.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TMapperDestination?> FirstOrDefaultAsync<TMapperDestination>(Specification<TEntity> specification, CancellationToken cancellationToken)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: DbSet,
            specification: specification
        );

        return await queryResult
            .ProjectTo<TMapperDestination>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity?> SingleOrDefaultAsync(Specification<TEntity> specification, CancellationToken cancellationToken)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: DbSet,
            specification: specification
        );

        return await queryResult.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity> FirstAsync(Specification<TEntity> specification, CancellationToken cancellationToken)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: DbSet,
            specification: specification
        );

        return await queryResult.FirstAsync(cancellationToken);
    }

    private bool _disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}