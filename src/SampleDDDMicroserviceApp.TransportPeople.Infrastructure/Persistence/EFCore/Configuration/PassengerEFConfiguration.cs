using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleDDDMicroserviceApp.TransportPeople.Domain.PassengerAggregate;
using SampleDDDMicroserviceApp.TransportPeople.Domain.PassengerAggregate.ValueObjects;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.User;
using SampleDDDMicroserviceApp.TransportPeople.Infrastructure.Persistence.DbConstants;

namespace SampleDDDMicroserviceApp.TransportPeople.Infrastructure.Persistence.EFCore.Configuration;

public class PassengerEFConfiguration : IEntityTypeConfiguration<Passenger>
{
    public void Configure(EntityTypeBuilder<Passenger> passengerBuilder)
    {
        passengerBuilder.ToTable(TableNameConst.Passengers);

        passengerBuilder.HasKey(p => p.Id);

        passengerBuilder.Property(p => p.Id).HasConversion(
            passengerId => passengerId!.Value,
            value => new PassengerId(value));

        passengerBuilder.HasMany(p => p.PassengerSmsMessages)
            .WithOne()
            .HasForeignKey(pm => pm.PassengerId);

        passengerBuilder.HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.Person!.UserId)
            .HasPrincipalKey(u => u.UserId);

        passengerBuilder.ComplexProperty(p => p.Person, b =>
        {
            b.Property(per => per!.FullName).IsRequired().HasMaxLength(Person.FullNameMaxLength);
            b.Property(per => per!.PhoneNo).HasMaxLength(11);
            b.Property(per => per!.NationalCode).HasMaxLength(11);
        });

        passengerBuilder.ComplexProperty(p => p.HomeAddress);

        passengerBuilder.ComplexProperty(p => p.WorkAddress);
    }
}

public class PassengerMessageEFConfiguration : IEntityTypeConfiguration<PassengerSmsMessage>
{
    public void Configure(EntityTypeBuilder<PassengerSmsMessage> smsBuilder)
    {
        smsBuilder.ToTable(TableNameConst.PassengerSmsMessages);

        smsBuilder.HasKey(p => p.Id);

        smsBuilder.Property(p => p.Id).HasConversion(
            passengerMessageId => passengerMessageId!.Value,
            value => new PassengerSmsMessageId(value));

        smsBuilder.HasOne<Passenger>()
            .WithMany()
            .HasForeignKey(pm => pm.PassengerId);

        smsBuilder.HasOne<CrewUser>()
            .WithMany()
            .HasForeignKey(pm => pm.SenderUserId)
            .HasPrincipalKey(u => u.UserId);
    }
}