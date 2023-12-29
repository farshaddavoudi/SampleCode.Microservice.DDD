using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.PassengerAggregate.DomainEvents;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.PassengerUserCases.CreatePassenger;

public class PassengerCreatedDomainEventHandler : IDomainEventHandler<PassengerCreatedDomainEvent>
{
    public async Task Handle(PassengerCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
    }
}