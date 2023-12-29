using NetCore.AutoRegisterDi;
using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.DiSetup;

/// <summary>
/// Register Assembly Public NonAbstract Classes 
/// </summary>
public class AutoRegisterDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        services.RegisterAssemblyPublicNonGenericClasses()
            .Where(c => c.Name.EndsWith("Service"))  //optional
        //    .IgnoreThisInterface<IMyInterface>()     //optional
              .AsPublicImplementedInterfaces();

        #region Scanning Multiple assemblies example

        //var assembliesToScan = new[]
        //{
        //    Assembly.GetExecutingAssembly(),
        //    Assembly.GetAssembly(typeof(MyServiceInAssembly1)),
        //    Assembly.GetAssembly(typeof(MyServiceInAssembly2))
        //};

        //services.RegisterAssemblyPublicNonGenericClasses(assembliesToScan)
        //    .Where(c => c.Name.EndsWith("Service"))  //optional
        //    .IgnoreThisInterface<IMyInterface>()     //optional
        //    .AsPublicImplementedInterfaces();

        #endregion
    }
}