namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.User;

public class CrewUser : User
{
    public string? CrewCode { get; set; }
    public string? ScheduleName { get; set; }
    public string? LicenceNo { get; set; }
}