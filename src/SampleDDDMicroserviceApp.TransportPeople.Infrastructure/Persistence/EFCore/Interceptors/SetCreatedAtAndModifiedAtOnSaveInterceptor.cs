using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Entity;

namespace SampleDDDMicroserviceApp.TransportPeople.Infrastructure.Persistence.EFCore.Interceptors;

public class SetCreatedAtAndModifiedAtOnSaveInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        ApplyAudits(eventData.Context?.ChangeTracker);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        ApplyAudits(eventData.Context?.ChangeTracker);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void ApplyAudits(ChangeTracker? changeTracker)
    {
        ApplyCreateAudit(changeTracker);
        ApplyUpdateAudit(changeTracker);
        ApplyDeleteAudit(changeTracker);
    }

    private void ApplyCreateAudit(ChangeTracker? changeTracker)
    {
        var addedEntries = changeTracker?.Entries()
            .Where(x => x.State == EntityState.Added);

        if (addedEntries != null)
        {
            foreach (var addedEntry in addedEntries)
            {
                if (addedEntry.Entity is BaseEntity entity)
                {
                    entity.SetCreatedAtToNow();
                }
            }
        }
    }

    private void ApplyUpdateAudit(ChangeTracker? changeTracker)
    {
        var modifiedEntries = changeTracker?.Entries()
            .Where(x => x.State == EntityState.Modified);

        if (modifiedEntries != null)
        {
            foreach (var modifiedEntry in modifiedEntries)
            {
                if (modifiedEntry.Entity is BaseEntity entity)
                {
                    entity.SetModifiedAtToNow();
                }
            }
        }
    }

    private void ApplyDeleteAudit(ChangeTracker? changeTracker)
    {
        var deletedEntries = changeTracker?.Entries()
            .Where(x => x.State == EntityState.Deleted);

        if (deletedEntries != null)
        {
            foreach (var modifiedEntry in deletedEntries)
            {
                if (modifiedEntry.Entity is BaseEntity entity)
                {
                    entity.SetModifiedAtToNow();
                }
            }
        }
    }
}