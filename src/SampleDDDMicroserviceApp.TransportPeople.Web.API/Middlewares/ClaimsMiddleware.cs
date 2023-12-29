using System.Security.Claims;
using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;

namespace SampleDDDMicroserviceApp.TransportPeople.Web.API.Middlewares;

public class ClaimsMiddleware : IMiddleware
{
    private readonly IUserClaimsService _userClaimsService;

    #region Constructor

    public ClaimsMiddleware(IUserClaimsService userClaimsService)
    {
        _userClaimsService = userClaimsService;
    }

    #endregion

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.User.Identity?.IsAuthenticated ?? false)
        {
            var userIdStr = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userIdStr) is false)
            {
                int userId = Convert.ToInt32(userIdStr);

                var userClaims = await _userClaimsService.GetUserClaimsAsync(userId, CancellationToken.None);

                // Attach retrieved claims to the current user
                var claimsIdentity = (ClaimsIdentity)context.User.Identity;

                claimsIdentity.AddClaims(userClaims);
            }
        }

        await next(context);
    }
}