using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.DriverAggregate;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Exceptions;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.DriverUseCases.ChangeDriverStatusToLeaveOfAbsence;

public record ChangeDriverStatusToLeaveOfAbsenceCommand(Guid DriverId) : IRequest;

public class ChangeDriverStatusToLeaveOfAbsenceCommandHandler(IRepository<Driver> driverRepository, IUnitOfWork unitOfWork) : IRequestHandler<ChangeDriverStatusToLeaveOfAbsenceCommand>
{
    public async Task Handle(ChangeDriverStatusToLeaveOfAbsenceCommand request, CancellationToken cancellationToken)
    {
        var driver = await driverRepository.GetByIdAsync(new DriverId(request.DriverId), cancellationToken);
        if (driver is null)
        {
            throw new NotFoundException("No driver was found");
        }

        driver.ChangeStatusToLeaveOfAbsence();

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}