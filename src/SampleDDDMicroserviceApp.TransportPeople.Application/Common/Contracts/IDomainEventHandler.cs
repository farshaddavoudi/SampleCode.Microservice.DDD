namespace SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent> where TDomainEvent : INotification
{

}