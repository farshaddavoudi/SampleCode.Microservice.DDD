using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Entity;

namespace SampleDDDMicroserviceApp.TransportPeople.Infrastructure.Persistence.EFCore.UnitOfWork;

public class UnitOfWork(AppDbContext dbContext, IPublisher publisher) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);

        await PublishDomainEventsAsync(dbContext.ChangeTracker, cancellationToken);
    }

    public void SaveChange()
    {
        dbContext.SaveChanges();

        PublishDomainEventsAsync(dbContext.ChangeTracker).GetAwaiter().GetResult();
    }

    private async Task PublishDomainEventsAsync(ChangeTracker dbContextChangeTracker, CancellationToken cancellationToken = default)
    {
        // TODO: Check if static field DomainEvent works as expected

        var domainEvents = dbContextChangeTracker.Entries<BaseEntity>()
            .Select(e => e.Entity)
            .Where(e => e.GetDomainEvents.Any())
            .SelectMany(e =>
            {
                var domainEvents = e.GetDomainEvents;

                e.ClearDomainEvents();

                return domainEvents;
            });

        foreach (var domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent, cancellationToken);
        }
    }

    private bool _disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
                dbContext.Dispose();
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}