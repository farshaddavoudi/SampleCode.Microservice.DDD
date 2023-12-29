using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Entity;

namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.User;

public class UserRoleView : IEntity
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? RoleName { get; set; }

    public string? ApplicationName { get; set; }
}