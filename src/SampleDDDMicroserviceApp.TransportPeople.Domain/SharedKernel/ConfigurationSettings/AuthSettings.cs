namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;

public record AuthSettings(string JwtSecret,
    string RefreshTokenHeaderName,
    string UserIdEncryptionKey);

