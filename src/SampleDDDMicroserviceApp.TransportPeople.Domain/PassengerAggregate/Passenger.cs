using SampleDDDMicroserviceApp.TransportPeople.Domain.PassengerAggregate.DomainEvents;
using SampleDDDMicroserviceApp.TransportPeople.Domain.PassengerAggregate.Enums;
using SampleDDDMicroserviceApp.TransportPeople.Domain.PassengerAggregate.Specifications;
using SampleDDDMicroserviceApp.TransportPeople.Domain.PassengerAggregate.ValueObjects;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Entity;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Exceptions;

namespace SampleDDDMicroserviceApp.TransportPeople.Domain.PassengerAggregate;

public class Passenger : Entity<PassengerId, int>, IAggregateRoot
{
    public Person? Person { get; private set; } //Don't remove private setters because in that case it won't map to Db columns anymore
    public Address? HomeAddress { get; private set; }
    public Address? WorkAddress { get; private set; }
    public PassengerType PassengerType { get; private set; }
    public string? Description { get; private set; }


    private readonly List<PassengerSmsMessage> _passengerSmsMessages = new();
    public IReadOnlyList<PassengerSmsMessage> PassengerSmsMessages => _passengerSmsMessages.ToList();

    private Passenger() { } //EF

    private Passenger(Person person, Address? homeAddress, Address? workAddress, PassengerType passengerType, string? description)
    {
        Person = person;
        HomeAddress = homeAddress;
        WorkAddress = workAddress;
        PassengerType = passengerType;
        Description = description;
    }

    public static async Task<Passenger?> CreateAsync(
        Person person,
        Address? homeAddress,
        Address? workAddress,
        PassengerType passengerType,
        string? description,
        IReadOnlyRepository<Passenger> passengerRepository,
        CancellationToken cancellationToken)
    {
        await GuardAgainstPassengerAlreadyExists(person, passengerRepository, cancellationToken);

        if (passengerType == default)
        {
            throw new BusinessRuleException("The passenger type not specified");
        }

        var passenger = new Passenger(person, homeAddress, workAddress, passengerType, description);

        RaiseDomainEvent(new PassengerCreatedDomainEvent(Guid.NewGuid(), passenger)); //Send the object so the auto increment [int] Id be populated after save 

        return passenger;
    }

    public void Remove()
    {
        // Logic to remove a passenger
        // Maybe raise a domain event

        SetArchived();
    }

    public void UpdatePhoneNo(string? phoneNo)
    {
        Person = Person.Create(Person!.UserId, Person.FullName, phoneNo, Person.NationalCode);
    }

    public void UpdateName(string name)
    {
        Person = Person.Create(Person!.UserId, name, Person.PhoneNo, Person.NationalCode);
    }

    public void CreateSmsMessage(string smsText, int? senderUserId)
    {
        var newSmsMessage = PassengerSmsMessage.Create(new PassengerSmsMessageId(Guid.NewGuid()), Id!, smsText, Person?.PhoneNo, senderUserId);

        _passengerSmsMessages.Add(newSmsMessage);
    }

    public async Task RemoveSmsAsync(PassengerSmsMessageId smsId, IReadOnlyRepository<Passenger> passengerRepository, CancellationToken cancellationToken)
    {
        // Check the smsMessageId with Double Dispatch pattern

        var passengerWithSms = await passengerRepository.FirstOrDefaultAsync(new PassengerWithSmsSpec(Id!, smsId), cancellationToken);
        if (passengerWithSms is null)
        {
            throw new BusinessRuleException("Passenger was not found");
        }

        if (passengerWithSms.PassengerSmsMessages.Count == 0)
        {
            throw new BusinessRuleException("SMS was not found");
        }

        var smsToDelete = passengerWithSms.PassengerSmsMessages.First();

        // Logic to remove the SMS message

        passengerWithSms._passengerSmsMessages.Remove(smsToDelete);
    }

    private static async Task GuardAgainstPassengerAlreadyExists(Person person, IReadOnlyRepository<Passenger> passengerRepository,
        CancellationToken cancellationToken)
    {
        bool passengerAlreadyExists = await passengerRepository.AnyAsync(
            new PassengerByUserIdOrPhoneNoSpec(person.UserId, person.PhoneNo),
            cancellationToken);

        if (passengerAlreadyExists)
        {
            throw new BusinessRuleException("Passenger is already existed");
        }
    }
}