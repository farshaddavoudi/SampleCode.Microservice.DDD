using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.DriverAggregate;
using SampleDDDMicroserviceApp.TransportPeople.Domain.DriverAggregate.Enums;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.User;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.DriverUseCases.CreateDriver;

public record CreateDriverCommand(int UserId, DriverType DriverType) : IRequest<Guid>;

public class CreateDriverCommandHandler(
    IRepository<Driver> driverRepository,
    IReadOnlyRepository<User> userRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateDriverCommand, Guid>
{
    public async Task<Guid> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
    {
        var newDriverId = Guid.NewGuid();

        var driver = await Driver.CreateAsync(
            new DriverId(newDriverId),
            request.UserId,
            request.DriverType,
            driverRepository,
            userRepository,
            cancellationToken);

        await driverRepository.AddAsync(driver, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return newDriverId;
    }
}