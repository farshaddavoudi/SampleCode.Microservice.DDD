using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;
using SampleDDDMicroserviceApp.TransportPeople.Infrastructure.UserIdentity;

namespace SampleDDDMicroserviceApp.TransportPeople.Web.API.DiSetup;

public class CurrentUserDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
    }
}