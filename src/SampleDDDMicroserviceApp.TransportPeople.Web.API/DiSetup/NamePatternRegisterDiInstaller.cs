using NetCore.AutoRegisterDi;
using SampleDDDMicroserviceApp.TransportPeople.Application;
using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;
using SampleDDDMicroserviceApp.TransportPeople.Infrastructure;

namespace SampleDDDMicroserviceApp.TransportPeople.Web.API.DiSetup;

public class NamePatternRegisterDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        var assemblies = new[] {
            typeof(ApiAssemblyEntryPoint).Assembly,
            typeof(InfrastructureAssemblyEntryPoint).Assembly,
            typeof(ApplicationAssemblyEntryPoint).Assembly,
            typeof(DomainAssemblyEntryPoint).Assembly
        };

        services.RegisterAssemblyPublicNonGenericClasses(assemblies)
            .Where(c => c.Name.EndsWith("Service"))
            .IgnoreThisInterface<ICurrentUserService>()     //optional
            .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
    }
}