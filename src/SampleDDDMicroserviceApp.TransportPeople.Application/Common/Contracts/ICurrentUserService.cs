using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.User;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;

public interface ICurrentUserService
{
    bool IsAuthenticated();

    MiniUser? User();

    bool IsAdmin();
}