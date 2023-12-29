using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Entity;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.User;

public class User : IEntity //=UserView
{
    public int UserId { get; set; }

    public int PersonnelCode { get; set; }

    // Below should be calculated on demand with below snippet
    // = File.Exists(@$"C:\SiteSource\cdn\signature\{PersonnelCode}.png");
    [NotMapped] public bool? HasSignature { get; set; }

    public int? RahkaranId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? FullName { get; set; }

    public string? FatherName { get; set; }

    public int? Gender { get; set; }
    public bool IsMale => Gender == 1;
    public bool IsFemale => !IsMale;

    public string? GenderDisplay { get; set; }

    public string? Mobile { get; set; }

    public int? WorkLocationCode { get; set; }

    public string? WorkLocation { get; set; }

    public string? NationalCode { get; set; }

    public DateTime? BirthDate { get; set; }
    public string BirthDateJalali => BirthDate.ToJalaliString();

    public int? MaritalStatus { get; set; }
    public string? MaritalStatusDisplay => MaritalStatus is 1 or 2 ? ((MaritalStatus)(int)MaritalStatus).ToDisplayName() : "نامشخص";

    public DateTime? EmploymentDate { get; set; }
    [NotMapped] public string EmploymentDateJalali => EmploymentDate.ToJalaliString();

    public int? EmploymentStatusId { get; set; }

    public string? EmploymentStatusTitle { get; set; }

    public bool Dismissed { get; set; }

    public int? PostId { get; set; }

    public string? PostTitle { get; set; }
}