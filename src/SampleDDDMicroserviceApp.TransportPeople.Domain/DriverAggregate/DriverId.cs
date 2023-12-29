using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Entity;

namespace SampleDDDMicroserviceApp.TransportPeople.Domain.DriverAggregate;

public sealed record DriverId(Guid Value) : IStronglyTypedId<Guid>;