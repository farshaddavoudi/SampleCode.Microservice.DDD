using Audit.EntityFramework;
using Audit.EntityFramework.Interceptors;
using Microsoft.EntityFrameworkCore;
using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Infrastructure.Persistence.EFCore;
using SampleDDDMicroserviceApp.TransportPeople.Infrastructure.Persistence.EFCore.Interceptors;
using SampleDDDMicroserviceApp.TransportPeople.Infrastructure.Persistence.EFCore.Repository;
using SampleDDDMicroserviceApp.TransportPeople.Infrastructure.Persistence.EFCore.UnitOfWork;

namespace SampleDDDMicroserviceApp.TransportPeople.Infrastructure.DiSetup;

public class DataAccessDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        services.AddScoped(typeof(IReadOnlyRepository<>), typeof(ReadOnlyEfRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAppDbContext, AppDbContext>();

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(appSettings.ConnStrSettings!.AppDbConnStr, sqlServerOptions =>
            {
                sqlServerOptions.CommandTimeout((int)TimeSpan.FromMinutes(1)
                    .TotalSeconds); //Default is 30 seconds
            });

            // Interceptors

            // High-level Interceptors
            options.AddInterceptors(new SetCreatedAtAndModifiedAtOnSaveInterceptor());

            if (appSettings.MongoDbSettings!.IsEnabled)
            {
                options.AddInterceptors(new AuditSaveChangesInterceptor());
            }

            if (appSettings.MongoDbSettings!.IsEnabled)
            {
                options.AddInterceptors(new AuditCommandInterceptor()
                {
                    LogParameterValues = true,
                    IncludeReaderResults = true,
                    AuditEventType = "EFCommand:{database}:{method}"
                });
            }

            // Show Detailed Errors
            if (appSettings.IsDevelopment)
                options.EnableSensitiveDataLogging().EnableDetailedErrors();
        });
    }

}