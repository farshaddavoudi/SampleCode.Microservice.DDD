namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Entity;

public class ArchivableEntity : IArchivableEntity
{
    protected ArchivableEntity() { } //EF

    public bool IsArchived { get; private set; }

    public void SetArchived()
    {
        IsArchived = true;
    }
}