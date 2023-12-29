using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;

namespace SampleDDDMicroserviceApp.TransportPeople.Web.API.Controllers.Admin;

public class AppSettingsJsonController(IConfiguration configuration, AppSettings appSettings) : BaseApiController
{
    [HttpGet]
    public IActionResult ApplyChanges()
    {
        var isDevelopment = appSettings.IsDevelopment;

        appSettings = configuration.Get<AppSettings>()!;

        appSettings.IsDevelopment = isDevelopment;

        return Ok(JsonSerializer.Serialize(appSettings));
    }
}