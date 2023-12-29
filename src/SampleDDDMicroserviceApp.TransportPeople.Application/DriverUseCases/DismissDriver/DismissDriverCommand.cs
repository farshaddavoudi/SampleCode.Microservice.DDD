using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.DriverAggregate;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Exceptions;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.DriverUseCases.DismissDriver;

public record DismissDriverCommand(Guid DriverId) : IRequest;

public class DismissDriverCommandHandler(IRepository<Driver> driverRepository, IUnitOfWork unitOfWork) : IRequestHandler<DismissDriverCommand>
{
    public async Task Handle(DismissDriverCommand request, CancellationToken cancellationToken)
    {
        var driver = await driverRepository.GetByIdAsync(new DriverId(request.DriverId), cancellationToken);
        if (driver is null)
        {
            throw new NotFoundException("No driver was found");
        }

        driver.Dismiss();

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}