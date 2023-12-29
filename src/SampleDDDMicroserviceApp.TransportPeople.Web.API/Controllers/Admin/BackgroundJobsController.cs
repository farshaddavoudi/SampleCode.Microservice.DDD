using Microsoft.AspNetCore.Mvc;
using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;

namespace SampleDDDMicroserviceApp.TransportPeople.Web.API.Controllers.Admin;

public class BackgroundJobsController(IAppBackgroundJobsService appBackgroundJobsService) : BaseApiController
{
    [HttpPost]
    public IActionResult ScheduleSyncInsuredsTableJob()
    {
        appBackgroundJobsService.ScheduleJob_SyncInsuredsTable();

        return NoContent();
    }

    [HttpPost]
    public IActionResult RemoveSyncInsuredsTableJob()
    {
        appBackgroundJobsService.RemoveJob_SyncInsuredsTable();

        return NoContent();
    }
}