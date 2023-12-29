using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.User;
using SampleDDDMicroserviceApp.TransportPeople.Infrastructure.Persistence.DbConstants;

namespace SampleDDDMicroserviceApp.TransportPeople.Infrastructure.Persistence.EFCore.Configuration;

public class UserEFConfiguration : IEntityTypeConfiguration<CrewUser>
{
    public void Configure(EntityTypeBuilder<CrewUser> userBuilder)
    {
        userBuilder.ToView(ViewNameConst.UsersView).HasKey(u => u.UserId);
    }
}

public class UserRoleEFConfiguration : IEntityTypeConfiguration<UserRoleView>
{
    public void Configure(EntityTypeBuilder<UserRoleView> userRoleBuilder)
    {
        userRoleBuilder.ToView(ViewNameConst.UserRolesView).HasKey(ur => ur.Id);

        userRoleBuilder.HasOne<CrewUser>()
            .WithMany()
            .HasForeignKey(ur => ur.UserId)
            .HasPrincipalKey(u => u.UserId);
    }
}