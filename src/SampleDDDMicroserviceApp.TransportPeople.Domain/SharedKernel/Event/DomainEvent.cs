using MediatR;

namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Event;

public record DomainEvent(Guid EventId) : INotification;