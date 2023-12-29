using Microsoft.EntityFrameworkCore.Storage;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;

public interface IAppDbContext
{
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
}