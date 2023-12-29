using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Event;

namespace SampleDDDMicroserviceApp.TransportPeople.Domain.DriverAggregate.DomainEvents;

public sealed record DriverCreatedDomainEvent(Guid EventId, DriverId DriverId) : DomainEvent(EventId);
