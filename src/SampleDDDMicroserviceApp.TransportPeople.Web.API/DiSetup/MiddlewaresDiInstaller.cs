using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;
using SampleDDDMicroserviceApp.TransportPeople.Web.API.Middlewares;

namespace SampleDDDMicroserviceApp.TransportPeople.Web.API.DiSetup;

public class MiddlewaresDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        services.AddTransient<GlobalExceptionHandlingMiddleware>();

        services.AddTransient<CorrelationIdHandlingMiddleware>();

        services.AddTransient<ClaimsMiddleware>();

        // Serilog
        services.AddTransient<AddCorrelationIdToSerilogContextMiddleware>();
        services.AddTransient<AddCurrentUserToSerilogContextMiddleware>();
        services.AddTransient<AddClientIpToSerilogContextMiddleware>();

        // Audit.NET
        services.AddTransient<AddCorrelationIdToAuditNetLogsMiddleware>();
        services.AddTransient<AddCurrentUserToAuditNetLogsMiddleware>();
        services.AddTransient<AddClientIpToAuditNetLogsMiddleware>();
    }
}