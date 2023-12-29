using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Constants;

namespace SampleDDDMicroserviceApp.TransportPeople.Web.API.DiSetup;

public class AuthorizationDiInstaller : IDiInstaller
{
    /// <summary>
    /// Create Policy in order to be used in the [Authorize] attribute on Controllers
    /// [Authorize] accepts Policy as [Authorize(Policy="MyPolicy")] and Roles but not Claims
    /// </summary>
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        services.AddAuthorization(options =>
        {
            //options.AddPolicy(Policy.Test, policy => policy.RequireClaim("TestClaim"));

            options.AddPolicy(PolicyConst.AdminAccessOnly, policy => policy.RequireRole(nameof(Administrator)));
        });
    }
}