namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Entity;

public interface IArchivableEntity : IEntity
{
    public bool IsArchived { get; }
}