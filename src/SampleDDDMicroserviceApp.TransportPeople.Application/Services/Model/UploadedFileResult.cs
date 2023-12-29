namespace SampleDDDMicroserviceApp.TransportPeople.Application.Services.Model;

public class UploadedFileResult
{
    public Guid Identifier { get; set; }

    public string? FileUrl { get; set; }
}