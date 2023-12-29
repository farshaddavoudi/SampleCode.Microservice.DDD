using System.ComponentModel.DataAnnotations;

namespace SampleDDDMicroserviceApp.TransportPeople.Domain.PassengerAggregate.Enums;

public enum PassengerType
{
    [Display(Name = "Pilot")]
    Pilot = 10,

    [Display(Name = "Co-pilot")]
    CoPilot = 20,

    [Display(Name = "Pursur")]
    Pursur = 30,

    [Display(Name = "Attendant")]
    Attendant = 40,

    [Display(Name = "Maintenance engineer")]
    MaintenanceEngineer = 50
}