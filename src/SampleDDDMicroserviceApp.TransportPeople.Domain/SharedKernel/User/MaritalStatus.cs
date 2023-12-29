using System.ComponentModel.DataAnnotations;

namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.User;

public enum MaritalStatus
{
    [Display(Name = "مجرد")]
    Bachelor = 1,

    [Display(Name = "متاهل")]
    Married = 2
}