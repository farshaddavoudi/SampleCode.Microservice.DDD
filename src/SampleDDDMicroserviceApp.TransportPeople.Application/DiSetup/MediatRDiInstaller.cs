using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Application.Common.PipelineBehaviours;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.DiSetup;

public class MediatRDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        services.AddMediatR(configs =>
        {
            configs.RegisterServicesFromAssemblies(typeof(ApplicationAssemblyEntryPoint).Assembly);

            configs.Lifetime = ServiceLifetime.Scoped;

            // Pipelines order matters. MediatR executes them from top to bottom 
            configs.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>), ServiceLifetime.Scoped);
            configs.AddBehavior(typeof(IPipelineBehavior<,>), typeof(DbTransactionBehaviour<,>), ServiceLifetime.Scoped);
        });
    }
}