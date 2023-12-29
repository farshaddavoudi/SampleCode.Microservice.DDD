using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.DriverAggregate;
using SampleDDDMicroserviceApp.TransportPeople.Domain.DriverAggregate.Enums;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Exceptions;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.DriverUseCases.UpdateDriverType;

public record UpdateDriverTypeCommand(Guid DriverId, DriverType NewDriverType) : IRequest;

public class UpdateDriverTypeCommandHandler(IRepository<Driver> driverRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateDriverTypeCommand>
{
    public async Task Handle(UpdateDriverTypeCommand request, CancellationToken cancellationToken)
    {
        var driver = await driverRepository.GetByIdAsync(new DriverId(request.DriverId), cancellationToken);
        if (driver is null)
        {
            throw new NotFoundException("No driver was found");
        }

        driver.ChangeDriverType(request.NewDriverType);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}