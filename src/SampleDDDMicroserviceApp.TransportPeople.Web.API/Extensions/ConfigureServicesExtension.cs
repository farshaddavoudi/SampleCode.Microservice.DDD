using SampleDDDMicroserviceApp.TransportPeople.Application;
using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;
using SampleDDDMicroserviceApp.TransportPeople.Infrastructure;

namespace SampleDDDMicroserviceApp.TransportPeople.Web.API.Extensions;

public static class ConfigureServicesExtension
{
    public static IServiceCollection ConfigureServicesWithInstallers(this IServiceCollection services, AppSettings appSettings)
    {
        var assemblies = new[] {
            typeof(ApiAssemblyEntryPoint).Assembly,
            typeof(InfrastructureAssemblyEntryPoint).Assembly,
            typeof(ApplicationAssemblyEntryPoint).Assembly
        };

        var installers = assemblies.SelectMany(a => a.GetExportedTypes())
            .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(IDiInstaller).IsAssignableFrom(c))
            .Select(Activator.CreateInstance).Cast<IDiInstaller>().ToList();

        installers.ForEach(i => i.InstallServices(services, appSettings));

        return services;
    }
}