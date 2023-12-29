using Hangfire.Dashboard;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Constants;

namespace SampleDDDMicroserviceApp.TransportPeople.Web.API.ActionFilter;

public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        return context.GetHttpContext().User.IsInRole(RoleConst.Identity_Administrator);
    }
}