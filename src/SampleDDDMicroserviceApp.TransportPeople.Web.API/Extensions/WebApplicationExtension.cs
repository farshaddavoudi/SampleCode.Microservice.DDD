using Audit.Core;
using Audit.WebApi;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;
using SampleDDDMicroserviceApp.TransportPeople.Infrastructure.Persistence.EFCore;

namespace SampleDDDMicroserviceApp.TransportPeople.Web.API.Extensions;

public static class WebApplicationExtension
{
    public static void ConfigureAuditNetLogging(this IApplicationBuilder app, AppSettings appSettings)
    {
        if (appSettings.MongoDbSettings!.IsEnabled)
        {
            // Configure the Entity framework audit.
            Audit.EntityFramework.Configuration.Setup()
            .ForContext<AppDbContext>(_ => _
                .AuditEventType("EFSaveChange:{database}")
                .IncludeEntityObjects())
            .UseOptOut();

            // Add the audit Middleware to the pipeline
            app.UseAuditMiddleware(_ => _
                .FilterByRequest(r => !r.Path.Value!.EndsWith("favicon.ico"))
                .WithEventType("API:{verb}:{controller}:{action}")
                .IncludeHeaders()
                .IncludeRequestBody()
                .IncludeResponseBody());

            // Configuring the audit output.
            // For more info, see https://github.com/thepirat000/Audit.NET#data-providers.

            Audit.Core.Configuration.Setup()
                .UseMongoDB(config => config
                    .ConnectionString(appSettings.MongoDbSettings!.ConnectionString)
                    .Database(appSettings.MongoDbSettings!.DatabaseName) //ata_{AppName}
                    .Collection(appSettings.MongoDbSettings!.CollectionName));
        }
    }

    public static void ConfigureHangfireDashboard(this IApplicationBuilder app, AppSettings appSettings)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            DashboardTitle = "CREW REQUEST JOBS",
            Authorization = new[]
            {
                new HangfireCustomBasicAuthenticationFilter
                {
                    User = "Administrator",
                    Pass = "Aa123456"
                }
            }
        });
    }
}