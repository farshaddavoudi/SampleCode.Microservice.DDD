namespace SampleDDDMicroserviceApp.TransportPeople.Domain.PassengerAggregate.Specifications;

public sealed class PassengerWithSmsSpec : Specification<Passenger>
{
    public PassengerWithSmsSpec(PassengerId passengerId, PassengerSmsMessageId passengerSmsMessageId)
    {
        Query
            .Include(p => p.PassengerSmsMessages.Where(pm => pm.Id == passengerSmsMessageId))
            .Where(p => p.Id == passengerId);
    }
}