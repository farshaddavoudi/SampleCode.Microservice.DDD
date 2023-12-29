
// ReSharper disable InvalidXmlDocComment

using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Entity;

namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Contracts;

public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class, IAggregateRoot
{
    Task AddAsync(TEntity itemToAdd, CancellationToken cancellationToken);

    void Add(TEntity itemToAdd);

    Task AddRangeAsync(IEnumerable<TEntity> entitiesToAdd, CancellationToken cancellationToken);

    void AddRange(IEnumerable<TEntity> entitiesToAdd);

    void Update(TEntity itemToUpdate);

    void Delete(TEntity itemToDelete);
}