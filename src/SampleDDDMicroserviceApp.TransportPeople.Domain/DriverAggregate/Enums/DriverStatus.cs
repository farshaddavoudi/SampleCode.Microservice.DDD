namespace SampleDDDMicroserviceApp.TransportPeople.Domain.DriverAggregate.Enums;

public enum DriverStatus
{
    Active = 10,

    // A leave of absence is when an employee is given permission to take time off from work for an extended period of time.
    // The time that's taken can either be paid, unpaid, mandatory or voluntary depending on the circumstances of the request.
    LeaveOfAbsence = 20,

    Dismissed = 30
}