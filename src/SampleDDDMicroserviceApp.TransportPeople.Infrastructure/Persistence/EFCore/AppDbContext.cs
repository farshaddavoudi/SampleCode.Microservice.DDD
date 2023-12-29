using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.User;
using SampleDDDMicroserviceApp.TransportPeople.Infrastructure.Persistence.EFCore.Extensions;

namespace SampleDDDMicroserviceApp.TransportPeople.Infrastructure.Persistence.EFCore;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Auto Register all Entities
        modelBuilder.RegisterDbSets(typeof(DomainAssemblyEntryPoint).Assembly);

        base.OnModelCreating(modelBuilder);

        // Seed Base Data to Database
        //modelBuilder.SeedDefaultRoles();

        // EF Core Global Query Filters
        modelBuilder.RegisterIsArchivedGlobalQueryFilter();

        modelBuilder.Entity<UserRoleView>().HasQueryFilter(ur => ur.ApplicationName == "AppName");

        // Restrict Delete (in Hard delete scenarios)
        // Ef default is Cascade
        modelBuilder.SetRestrictAsDefaultDeleteBehavior();

        // Auto Register all Entity Configurations (Fluent-API)
        modelBuilder.ApplyConfigurations(typeof(InfrastructureAssemblyEntryPoint).Assembly);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return await Database.BeginTransactionAsync(cancellationToken);
    }
}