using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.PassengerAggregate;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Exceptions;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.PassengerUserCases.UpdatePassengerPhoneNo;

public record UpdatePassengerPhoneNoCommand(int PassengerId, string? NewPhoneNo) : IRequest;

public class UpdatePassengerPhoneNoCommandHandler(
    IRepository<Passenger> passengerRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdatePassengerPhoneNoCommand>
{
    public async Task Handle(UpdatePassengerPhoneNoCommand request, CancellationToken cancellationToken)
    {
        var passenger = await passengerRepository.GetByIdAsync(new PassengerId(request.PassengerId), cancellationToken);
        if (passenger is null)
        {
            throw new BusinessRuleException("No passenger was found");
        }

        passenger.UpdatePhoneNo(request.NewPhoneNo);

        passengerRepository.Update(passenger); //Changing the ValueObject isn't tracked by EF Core

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}