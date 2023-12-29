using System.Security.Claims;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;

public interface IUserClaimsService
{
    Task<IEnumerable<Claim>> GetUserClaimsAsync(int userId, CancellationToken cancellationToken);
}