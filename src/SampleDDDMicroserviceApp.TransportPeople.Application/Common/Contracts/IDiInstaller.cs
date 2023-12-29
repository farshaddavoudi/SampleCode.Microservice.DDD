using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;

public interface IDiInstaller
{
    void InstallServices(IServiceCollection services, AppSettings appSettings);
}