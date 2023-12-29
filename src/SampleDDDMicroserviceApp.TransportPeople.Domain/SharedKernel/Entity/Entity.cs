namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Entity;

public abstract class Entity<TStronglyTypedId, TDbKey> : BaseEntity where TStronglyTypedId : class,
    IStronglyTypedId<TDbKey> where TDbKey : unmanaged
{
    protected Entity() { } //EF

    // Shared Props

    public TStronglyTypedId? Id { get; protected set; }

    public DateTime CreatedAt { get; protected set; }

    public DateTime? UpdatedAt { get; protected set; }

    // Methods 

    public void SetCreatedAt()
    {
        CreatedAt = DateTime.Now;
    }

    public void SetUpdatedAt()
    {
        UpdatedAt = DateTime.Now;
    }
}