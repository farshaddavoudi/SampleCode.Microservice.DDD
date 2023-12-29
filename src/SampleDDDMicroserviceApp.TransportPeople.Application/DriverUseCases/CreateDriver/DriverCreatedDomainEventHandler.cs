using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.DriverAggregate.DomainEvents;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.DriverUseCases.CreateDriver;

public sealed class DriverCreatedDomainEventHandler : IDomainEventHandler<DriverCreatedDomainEvent>
{
    public async Task Handle(DriverCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        // We can publish integration event here e.g. by RabbitMQ
    }
}