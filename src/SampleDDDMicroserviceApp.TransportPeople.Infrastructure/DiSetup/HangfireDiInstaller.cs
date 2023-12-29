using Hangfire;
using Hangfire.PostgreSql;
using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Constants;
using SampleDDDMicroserviceApp.TransportPeople.Infrastructure.BackgroundJobs;

namespace SampleDDDMicroserviceApp.TransportPeople.Infrastructure.DiSetup;

public class HangfireDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        // Add Hangfire services.
        services.AddHangfire(config =>
        {
            config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(c => c.UseNpgsqlConnection(appSettings.ConnStrSettings!.AppDbConnStr));
        });

        // Add the processing server as IHostedService
        services.AddHangfireServer(options =>
        {
            var env = appSettings.IsDevelopment ? "DEV" : "PROD";

            options.ServerName = $"{AppMetadataConst.SolutionName.ToUpper()}–{env.ToUpper()}–SERVER";
            options.Queues = new[] { HangfireConst.Queue.DefaultQueue };
        });
    }
}