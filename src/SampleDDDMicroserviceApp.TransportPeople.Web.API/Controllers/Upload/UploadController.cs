using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleDDDMicroserviceApp.TransportPeople.Application.Services.Model;
using SampleDDDMicroserviceApp.TransportPeople.Application.ServicesContracts;

namespace SampleDDDMicroserviceApp.TransportPeople.Web.API.Controllers.Upload;

public class UploadController : BaseApiController
{
    private readonly IUploaderService _uploaderService;
    private readonly ILogger<UploadController> _logger;

    #region Constructor Injections
    public UploadController(ILogger<UploadController> logger, IUploaderService uploaderService)
    {
        _logger = logger;
        _uploaderService = uploaderService;
    }
    #endregion

    [HttpPost]
    [DisableRequestSizeLimit]
    [AllowAnonymous]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Create))]
    public async Task<IActionResult> UploadFile([FromForm] IFormFile file, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start uploading the file");

        var fileName = Request.Headers[nameof(UploadRequestData.FileName)];
        var folderName = Request.Headers[nameof(UploadRequestData.FolderName)];
        var subfolderName = Request.Headers[nameof(UploadRequestData.SubfolderName)];

        var result = await _uploaderService.Upload(file, fileName, folderName, subfolderName, cancellationToken);

        _logger.LogInformation("Finish uploading the file");

        return Created(result);
    }
}