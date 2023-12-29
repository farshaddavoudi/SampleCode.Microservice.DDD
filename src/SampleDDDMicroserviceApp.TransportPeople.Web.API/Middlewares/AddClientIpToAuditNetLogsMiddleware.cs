using Audit.Core;
using SampleDDDMicroserviceApp.TransportPeople.Web.API.Extensions;

namespace SampleDDDMicroserviceApp.TransportPeople.Web.API.Middlewares;

public class AddClientIpToAuditNetLogsMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var clientIp = context.GetClientIp();

        Audit.Core.Configuration.AddCustomAction(ActionType.OnScopeCreated, scope =>
        {
            scope.SetCustomField("IP", clientIp ?? "unknown");
        });

        await next(context);
    }
}