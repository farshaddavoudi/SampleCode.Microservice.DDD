using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Implementations;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.DiSetup;

public class CorrelationIdManagerDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        services.AddScoped<ICorrelationIdManager, CorrelationIdManager>();
    }
}