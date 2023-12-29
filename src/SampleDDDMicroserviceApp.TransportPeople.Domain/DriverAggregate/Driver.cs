using SampleDDDMicroserviceApp.TransportPeople.Domain.DriverAggregate.DomainEvents;
using SampleDDDMicroserviceApp.TransportPeople.Domain.DriverAggregate.Enums;
using SampleDDDMicroserviceApp.TransportPeople.Domain.DriverAggregate.Specifications;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Entity;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Exceptions;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.User;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.User.Specifications;

namespace SampleDDDMicroserviceApp.TransportPeople.Domain.DriverAggregate;

public sealed class Driver : Entity<DriverId, Guid>, IAggregateRoot
{
    public int UserId { get; private set; }

    public DriverType DriverType { get; private set; }

    public DriverStatus DriverStatus { get; private set; }

    private Driver() { } //EF

    private Driver(DriverId id, int userId, DriverType driverType)
    {
        Id = id;
        UserId = userId;
        DriverType = driverType;
        DriverStatus = DriverStatus.Active;
    }

    public static async Task<Driver> CreateAsync(
        DriverId id,
        int userId,
        DriverType driverType,
        IReadOnlyRepository<Driver> driveRepository,
        IReadOnlyRepository<User> userRepository,
        CancellationToken cancellationToken = default)
    {
        await GuardAgainstInvalidUserId(userId, userRepository, cancellationToken);

        await GuardAgainstDriverAlreadyExists(userId, driveRepository, cancellationToken);

        if (driverType == default)
        {
            throw new BusinessRuleException("The driver type isn't specified");
        }

        var driver = new Driver(id, userId, driverType);

        RaiseDomainEvent(new DriverCreatedDomainEvent(Guid.NewGuid(), id));

        return driver;
    }

    public void Dismiss()
    {
        DriverStatus = DriverStatus.Dismissed;
    }

    public void ChangeStatusToLeaveOfAbsence()
    {
        DriverStatus = DriverStatus.LeaveOfAbsence;
    }

    public void ChangeDriverType(DriverType driverType)
    {
        if (DriverStatus is DriverStatus.Dismissed)
        {
            throw new BusinessRuleException("A dismissed driver type cannot be modified");
        }

        DriverType = driverType;
    }

    public void Remove()
    {
        // Validations to remove a driver

        SetArchived();
    }

    private static async Task GuardAgainstInvalidUserId(int userId, IReadOnlyRepository<User> userRepository,
        CancellationToken cancellationToken)
    {
        var userExists = await userRepository.AnyAsync(new UserByIdSpec(userId), cancellationToken);
        if (userExists is false)
        {
            throw new BusinessRuleException("The user is not a valid user");
        }
    }

    private static async Task GuardAgainstDriverAlreadyExists(int userId, IReadOnlyRepository<Driver> driveRepository,
        CancellationToken cancellationToken)
    {
        var driverAlreadyExists = await driveRepository.AnyAsync(new DriverByUserIdSpec(userId), cancellationToken);
        if (driverAlreadyExists)
        {
            throw new BusinessRuleException("The driver already exists");
        }
    }
}