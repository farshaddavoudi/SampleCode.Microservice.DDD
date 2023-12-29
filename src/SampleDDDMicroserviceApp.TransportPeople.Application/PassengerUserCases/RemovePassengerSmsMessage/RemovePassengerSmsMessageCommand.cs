using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.PassengerAggregate;
using SampleDDDMicroserviceApp.TransportPeople.Domain.PassengerAggregate.Specifications;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Contracts;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.PassengerUserCases.RemovePassengerSmsMessage;

public record RemovePassengerSmsMessageCommand(PassengerId PassengerId, PassengerSmsMessageId SmsId) : IRequest;

public class RemoveMessageCommandHandler(IRepository<Passenger> passengerRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<RemovePassengerSmsMessageCommand>
{
    public async Task Handle(RemovePassengerSmsMessageCommand request, CancellationToken cancellationToken)
    {
        var passenger = await passengerRepository.FirstOrDefaultAsync(
            new PassengerWithSmsSpec(request.PassengerId, request.SmsId),
            cancellationToken);

        if (passenger is not null)
        {
            await passenger.RemoveSmsAsync(request.SmsId, passengerRepository, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}