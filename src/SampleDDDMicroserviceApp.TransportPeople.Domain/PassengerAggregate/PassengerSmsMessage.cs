using DNTPersianUtils.Core;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Entity;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Exceptions;

namespace SampleDDDMicroserviceApp.TransportPeople.Domain.PassengerAggregate;

public class PassengerSmsMessage : Entity<PassengerSmsMessageId, Guid>
{
    public PassengerId? PassengerId { get; private set; }
    public DateTime MessagedAt { get; private set; }
    public string? Text { get; private set; }
    public string? PhoneNoSentTo { get; private set; }
    public int? SenderUserId { get; private set; }

    private PassengerSmsMessage() { } //EF

    private PassengerSmsMessage(PassengerSmsMessageId id, PassengerId? passengerId, string? text, string? phoneNoSentTo, int? senderUserId)
    {
        Id = id;
        PassengerId = passengerId;
        MessagedAt = DateTime.Now;
        Text = text;
        PhoneNoSentTo = phoneNoSentTo;
        SenderUserId = senderUserId;
    }

    public static PassengerSmsMessage Create(PassengerSmsMessageId id, PassengerId passengerId, string? text, string? phoneNoSentTo, int? senderUserId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text, nameof(text));

        ArgumentException.ThrowIfNullOrWhiteSpace(phoneNoSentTo, nameof(phoneNoSentTo));

        if (phoneNoSentTo.IsValidIranianMobileNumber() is false)
        {
            throw new BusinessRuleException("Phone number is invalid");
        }

        return new PassengerSmsMessage(id, passengerId, text, phoneNoSentTo, senderUserId);
    }
}