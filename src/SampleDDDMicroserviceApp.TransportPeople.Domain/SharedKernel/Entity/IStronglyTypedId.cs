namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Entity;

public interface IStronglyTypedId<T> where T : unmanaged
{
    public T Value { get; init; }
}