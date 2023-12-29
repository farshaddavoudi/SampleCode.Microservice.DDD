using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SampleDDDMicroserviceApp.TransportPeople.Web.API.ActionFilter;

public class CustomAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    public string[] Claims { get; set; } = Array.Empty<string>();
    public new string[] Roles { get; set; } = Array.Empty<string>();

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        // TODO
    }
}