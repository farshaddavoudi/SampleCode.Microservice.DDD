namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.User.Specifications;

public sealed class UserByIdSpec : Specification<User>
{
    public UserByIdSpec(int userId)
    {
        Query.Where(u => u.UserId == userId);
    }
}