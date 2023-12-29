namespace SampleDDDMicroserviceApp.TransportPeople.Application.Services;

public interface ISmsService
{
    Task SendAsync(string mobile, string text, CancellationToken cancellationToken);
}

public class SmsService : ISmsService
{
    public async Task SendAsync(string mobile, string text, CancellationToken cancellationToken)
    {
        // Send SMS message here

        await Task.CompletedTask;
    }
}