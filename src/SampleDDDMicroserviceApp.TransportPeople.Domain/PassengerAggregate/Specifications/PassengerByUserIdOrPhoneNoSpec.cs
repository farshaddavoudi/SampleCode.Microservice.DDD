namespace SampleDDDMicroserviceApp.TransportPeople.Domain.PassengerAggregate.Specifications;

public sealed class PassengerByUserIdOrPhoneNoSpec : Specification<Passenger>
{
    public PassengerByUserIdOrPhoneNoSpec(int? userId, string? phoneNo)
    {
        Query.Where(p => p.Person!.UserId == userId || p.Person.PhoneNo == phoneNo);
    }
}