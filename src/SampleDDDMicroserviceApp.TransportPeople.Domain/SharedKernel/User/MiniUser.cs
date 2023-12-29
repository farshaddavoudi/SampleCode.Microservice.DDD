namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.User;

/// <summary>
/// Information can be extracted for current user
/// </summary>
public class MiniUser
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public int PersonnelCode { get; set; }

    public string? UnitName { get; set; }

    public string? PostTitle { get; set; }

    public int? BoxId { get; set; }
    public bool HasBox => BoxId.HasValue;

    public int? WorkLocationCode { get; set; }

    public List<string> Roles { get; set; } = new();
}