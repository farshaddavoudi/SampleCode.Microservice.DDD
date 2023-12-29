namespace SampleDDDMicroserviceApp.TransportPeople.Domain.DriverAggregate.Specifications;

public sealed class DriverByUserIdSpec : Specification<Driver>
{
    public DriverByUserIdSpec(int userId)
    {
        Query.Where(d => d.UserId == userId);
    }
}