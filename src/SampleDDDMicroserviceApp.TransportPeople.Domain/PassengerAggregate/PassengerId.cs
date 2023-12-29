using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Entity;

namespace SampleDDDMicroserviceApp.TransportPeople.Domain.PassengerAggregate;

public sealed record PassengerId(int Value) : IStronglyTypedId<int>;