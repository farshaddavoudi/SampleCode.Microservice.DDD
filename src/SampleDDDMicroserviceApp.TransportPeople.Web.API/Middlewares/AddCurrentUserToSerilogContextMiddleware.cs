using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using Serilog.Context;

namespace SampleDDDMicroserviceApp.TransportPeople.Web.API.Middlewares;

public class AddCurrentUserToSerilogContextMiddleware : IMiddleware
{
    private readonly ICurrentUserService _currentUserService;

    public AddCurrentUserToSerilogContextMiddleware(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        using (LogContext.PushProperty("User", _currentUserService.User(), true))
        {
            await next(context);
        }
    }
}