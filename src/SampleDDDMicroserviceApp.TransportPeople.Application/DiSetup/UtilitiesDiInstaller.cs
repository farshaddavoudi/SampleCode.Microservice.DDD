using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Utilities;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.DiSetup;

public class UtilitiesDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        services.AddSingleton<CryptoUtility>();
    }
}