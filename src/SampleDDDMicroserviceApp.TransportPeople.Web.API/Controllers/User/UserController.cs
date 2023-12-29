using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Profiling;

namespace SampleDDDMicroserviceApp.TransportPeople.Web.API.Controllers.User;

public class UserController : BaseApiController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCurrentUserData(CancellationToken cancellationToken)
    {
        using (MiniProfiler.Current.Step("Get User Data"))
        {
            // var currentUser = await _mediator.Send(new GetCurrentUserDataQuery(), cancellationToken);

            return Ok();
        }
    }

    [HttpGet, AllowAnonymous]
    public IActionResult GetIp()
    {
        var actualIp = HttpContext.Connection.RemoteIpAddress?.ToString();

        var proxyIp = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? "no-proxy-ip-header";

        var xrealIp = HttpContext.Request.Headers["X-Real-IP"].FirstOrDefault() ?? "no-proxy-ip-header xreal";

        var res = $"actualIp : {actualIp} |||||| proxyIp : {proxyIp}   |||||||| proxId(x-real) : {xrealIp}";

        return Ok(res);
    }

    [HttpGet, AllowAnonymous]
    public IActionResult GetHeaders()
    {
        var headers = HttpContext.Request.Headers;

        return Ok(headers);
    }
}