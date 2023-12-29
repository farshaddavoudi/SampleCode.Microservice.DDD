using System.Globalization;
using Microsoft.AspNetCore.Localization;
using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Implementations;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.DiSetup;

public class LocalizationDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        services.AddLocalization();

        services.AddRequestLocalization(options =>
        {
            var supportedCultures = new[]
            {
                new CultureInfo("fa"),
                new CultureInfo("en")
            };

            options.SupportedCultures = supportedCultures;
            options.DefaultRequestCulture = new RequestCulture("fa");
            options.ApplyCurrentCultureToResponseHeaders = true;
            options.SupportedUICultures = supportedCultures;
            options.RequestCultureProviders = new List<IRequestCultureProvider>
            {
                new QueryStringRequestCultureProvider(),
                new AcceptLanguageHeaderRequestCultureProvider()
                // new CookieRequestCultureProvider()
            };
        });

        services.AddScoped<ILocalStringProvider, LocalStringProvider>();

        //services.AddSingleton<IConfigureOptions<MvcOptions>, MvcConfigurationToProvideModelBindingMessage>();
    }
}