using Microsoft.AspNetCore.Http;
using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Extensions;
using SampleDDDMicroserviceApp.TransportPeople.Application.Services.Model;
using SampleDDDMicroserviceApp.TransportPeople.Application.ServicesContracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Constants;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Exceptions;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Resources.ExceptionMessages;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.Services;

public class UploaderService : IUploaderService
{
    public async Task<UploadedFileResult> Upload(IFormFile file, string? fileName, string? folderName, string? subFolderName, CancellationToken cancellationToken)
    {
        var identifier = Guid.NewGuid();

        if (file.Length == 0) throw new ArgumentNullException(nameof(file));

        bool isPdf = Path.GetExtension(file.FileName).ToLower() == ".pdf";

        var allowedContentTypes = new List<string> { "application/pdf", "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" };

        if (file.IsImage() is false && isPdf is false && allowedContentTypes.Contains(file.ContentType.ToLower().Trim()) is false)
            throw new BadRequestException(ExceptionStrings.Uploader_InvalidFileFormat);

        if (file.Length > AppConst.UploadLimits.DocMaxSizeAllowedToUploadInKiloBytes * 1000)
            throw new BadRequestException(ExceptionStrings.Uploader_SizeExceeded);

        await using var memoryStream = new MemoryStream();

        await file.CopyToAsync(memoryStream, cancellationToken);

        // Upload file to the server and gets the saved URL

        return new UploadedFileResult
        {
            Identifier = identifier,
            FileUrl = "filePath"
        };
    }
}