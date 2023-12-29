using System.Reflection;
using FluentValidation;
using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.DiSetup;

public class FluentValidationDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}