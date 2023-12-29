using Audit.Core;
using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;

namespace SampleDDDMicroserviceApp.TransportPeople.Web.API.Middlewares;

public class AddCurrentUserToAuditNetLogsMiddleware(ICurrentUserService currentUserService) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // Include the trace identifier in the audit events
        if (context.User.Identity?.IsAuthenticated ?? false)
        {
            Audit.Core.Configuration.AddCustomAction(ActionType.OnScopeCreated, scope =>
            {
                scope.SetCustomField("UserId", currentUserService.User()?.Id);
                scope.SetCustomField("User", currentUserService.User(), true);
            });
        }

        await next(context);
    }
}