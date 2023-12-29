using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.PassengerAggregate;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Exceptions;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.PassengerUserCases.RemovePassenger;

public record RemovePassengerCommand(int PassengerId) : IRequest;


public class RemovePassengerCommandHandler(IRepository<Passenger> passengerRepository, IUnitOfWork unitOfWork) : IRequestHandler<RemovePassengerCommand>
{
    public async Task Handle(RemovePassengerCommand request, CancellationToken cancellationToken)
    {
        var passenger = await passengerRepository.GetByIdAsync(new PassengerId(request.PassengerId), cancellationToken);
        if (passenger is null)
        {
            throw new NotFoundException("No passenger was found");
        }

        passenger.Remove();

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}