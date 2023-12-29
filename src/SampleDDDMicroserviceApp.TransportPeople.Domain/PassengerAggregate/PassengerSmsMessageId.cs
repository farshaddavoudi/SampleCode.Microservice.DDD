using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Entity;

namespace SampleDDDMicroserviceApp.TransportPeople.Domain.PassengerAggregate;

public sealed record PassengerSmsMessageId(Guid Value) : IStronglyTypedId<Guid>;