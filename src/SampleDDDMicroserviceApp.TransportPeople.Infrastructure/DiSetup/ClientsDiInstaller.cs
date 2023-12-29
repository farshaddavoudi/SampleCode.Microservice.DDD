using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;

// ReSharper disable once ObjectCreationAsStatement

namespace SampleDDDMicroserviceApp.TransportPeople.Infrastructure.DiSetup;

public class ClientsDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        //// SSO Client
        //services.AddHttpClient<SSOClient>(client =>
        //{
        //    client.BaseAddress = new Uri(serverAppSettings.URLAppSetting!.SSOClient!);
        //});

        //// HR Client
        //services.AddHttpClient<HRClient>(client =>
        //{
        //    string hrClientBaseUrl = AspNetCoreAppEnvironmentsProvider.Current.HostingEnvironment.IsDevelopment()
        //        ? "https://dummy.com/" //Doesn't matter
        //        : serverAppSettings.URLAppSetting!.HRClient!;

        //    client.BaseAddress = new Uri(hrClientBaseUrl);
        //    client.DefaultRequestHeaders.Add("api_key", serverAppSettings.HRClientOptions?.APIKey);
        //});
    }
}