using SampleDDDMicroserviceApp.TransportPeople.Web.API.Extensions;
using Serilog.Context;

namespace SampleDDDMicroserviceApp.TransportPeople.Web.API.Middlewares;

public class AddClientIpToSerilogContextMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var clientIp = context.GetClientIp();

        using (LogContext.PushProperty("IP", clientIp ?? "unknown", true))
        {
            await next(context);
        }
    }
}