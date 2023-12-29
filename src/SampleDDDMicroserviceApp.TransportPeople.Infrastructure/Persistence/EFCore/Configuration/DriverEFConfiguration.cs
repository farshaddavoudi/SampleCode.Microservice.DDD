using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleDDDMicroserviceApp.TransportPeople.Domain.DriverAggregate;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.User;
using SampleDDDMicroserviceApp.TransportPeople.Infrastructure.Persistence.DbConstants;
using SampleDDDMicroserviceApp.TransportPeople.Infrastructure.Persistence.EFCore.Extensions;

namespace SampleDDDMicroserviceApp.TransportPeople.Infrastructure.Persistence.EFCore.Configuration;

public class DriverEFConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> driverBuilder)
    {
        driverBuilder.ToTable(TableNameConst.Drivers);

        driverBuilder.HasKey(d => d.Id);

        driverBuilder.Property(d => d.Id).HasConversion(
            driverId => driverId!.Value,
            value => new DriverId(value));

        driverBuilder.HasUniqueIndexArchivable(d => d.UserId);

        driverBuilder.HasOne<CrewUser>()
            .WithMany()
            .HasForeignKey(d => d.UserId)
            .HasPrincipalKey(u => u.UserId);
    }
}