namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;

public record RedisSettings(bool IsEnabled, string ConnectionString);