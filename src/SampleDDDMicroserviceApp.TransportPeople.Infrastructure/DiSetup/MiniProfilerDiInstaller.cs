using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;

namespace SampleDDDMicroserviceApp.TransportPeople.Infrastructure.DiSetup;

public class MiniProfilerDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        if (appSettings.IsDevelopment)
        {
            services.AddMemoryCache();

            services.AddMiniProfiler(options =>
                {
                    options.RouteBasePath = "/profiler"; /* baseUrl/profiler/results-index */

                    // More options on the MiniProfiler website:
                    // https://miniprofiler.com/dotnet/AspDotNetCore
                })
                .AddEntityFramework();
        }
    }
}