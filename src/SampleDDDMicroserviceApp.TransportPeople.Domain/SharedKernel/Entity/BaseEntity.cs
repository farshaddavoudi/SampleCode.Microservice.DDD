using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Event;

namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Entity;

public abstract class BaseEntity : ArchivableEntity
{
    public DateTime CreatedAt { get; protected set; } = DateTime.Now;

    public DateTime? ModifiedAt { get; protected set; }

    protected BaseEntity() { } //EF

    public void SetCreatedAtToNow()
    {
        CreatedAt = DateTime.Now;
    }

    public void SetModifiedAtToNow()
    {
        ModifiedAt = DateTime.Now;
    }

    // Event

    private static readonly List<DomainEvent> DomainEvents = new();

    public IReadOnlyList<DomainEvent> GetDomainEvents => DomainEvents.ToList();

    public void ClearDomainEvents() => DomainEvents.Clear();

    protected static void RaiseDomainEvent(DomainEvent domainEvent)
    {
        DomainEvents.Add(domainEvent);
    }
}