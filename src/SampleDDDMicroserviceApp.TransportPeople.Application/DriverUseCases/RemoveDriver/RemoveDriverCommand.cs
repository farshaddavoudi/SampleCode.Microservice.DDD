using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.DriverAggregate;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Exceptions;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.DriverUseCases.RemoveDriver;

public record RemoveDriverCommand(Guid DriverId) : IRequest;

public class RemoveDriverCommandHandler(IRepository<Driver> driverRepository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveDriverCommand>
{
    public async Task Handle(RemoveDriverCommand request, CancellationToken cancellationToken)
    {
        var driver = await driverRepository.GetByIdAsync(new DriverId(request.DriverId), cancellationToken);
        if (driver is null)
        {
            throw new NotFoundException("No driver was found");
        }

        driver.Remove();

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}