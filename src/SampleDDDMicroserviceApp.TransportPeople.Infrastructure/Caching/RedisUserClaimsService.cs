using Microsoft.Extensions.Caching.Distributed;
using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Exceptions;
using System.Security.Claims;

namespace SampleDDDMicroserviceApp.TransportPeople.Infrastructure.Caching;

public class RedisUserClaimsService : IUserClaimsService
{
    private readonly IDistributedCache _cache;
    private readonly ICurrentUserService _currentUserService;
    private readonly AppSettings _appSettings;

    #region Constructor

    public RedisUserClaimsService(IDistributedCache cache, ICurrentUserService currentUserService, AppSettings appSettings)
    {
        // TODO: Mock the IDistributedCache for when the Redis is disabled
        _cache = cache;
        _currentUserService = currentUserService;
        _appSettings = appSettings;
    }

    #endregion

    public async Task<IEnumerable<Claim>> GetUserClaimsAsync(int userId, CancellationToken cancellationToken)
    {
        string? ssoTokenClaim;

        if (_appSettings.RedisSettings!.IsEnabled is false)
        {
            ssoTokenClaim = await GetSsoTokenFromSecurityAsync(userId, cancellationToken);
        }
        else
        {
            // First, try to get from Redis
            ssoTokenClaim = await _cache.GetStringAsync($"userId-{userId}-SsoToken", token: cancellationToken);

            if (string.IsNullOrWhiteSpace(ssoTokenClaim))
            {
                // If nothing found in Redis, go and get the token directly by an API call
                ssoTokenClaim = await GetSsoTokenFromSecurityAsync(userId, cancellationToken);

                var expireOption = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(4) };

                await _cache.SetStringAsync($"userId-{userId}-SsoToken", ssoTokenClaim, expireOption, cancellationToken);
            }
        }

        return new Claim[]
        {
            new("token", ssoTokenClaim)
        };
    }

    private async Task<string> GetSsoTokenFromSecurityAsync(int userId, CancellationToken cancellationToken)
    {
        var ssoTokenClaim = "";

        if (ssoTokenClaim == null)
            throw new NotFoundException($"Cannot get any SsoToken from Security for the user – userId={userId}");

        return ssoTokenClaim;
    }
}