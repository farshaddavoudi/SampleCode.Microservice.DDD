namespace SampleDDDMicroserviceApp.TransportPeople.Application.Services.Model;

public class UploadRequestData
{
    public string? FileName { get; set; }

    public string? FolderName { get; set; }

    public string? SubfolderName { get; set; }
}