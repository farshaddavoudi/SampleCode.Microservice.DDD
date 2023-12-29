using Microsoft.AspNetCore.Http;
using SampleDDDMicroserviceApp.TransportPeople.Application.Services.Model;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.ServicesContracts;

public interface IUploaderService
{
    Task<UploadedFileResult> Upload(IFormFile file, string? fileName, string? folderName, string? subFolderName, CancellationToken cancellationToken);
}