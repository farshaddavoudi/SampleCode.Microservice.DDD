using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Event;

namespace SampleDDDMicroserviceApp.TransportPeople.Domain.PassengerAggregate.DomainEvents;

public record PassengerCreatedDomainEvent(Guid EventId, Passenger Passenger) : DomainEvent(EventId);