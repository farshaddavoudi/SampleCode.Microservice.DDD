namespace SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    void SaveChange();
}