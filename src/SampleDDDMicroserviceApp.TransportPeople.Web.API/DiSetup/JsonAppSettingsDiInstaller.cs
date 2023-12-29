using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;

namespace SampleDDDMicroserviceApp.TransportPeople.Web.API.DiSetup;

public class JsonAppSettingsDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        // Register (Server)AppSettings as Singleton to easy use
        services.AddSingleton(sp => appSettings);
    }
}