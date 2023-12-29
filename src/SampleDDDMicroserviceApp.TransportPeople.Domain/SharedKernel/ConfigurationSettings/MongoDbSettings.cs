namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;

public record MongoDbSettings(
    bool IsEnabled,
    string? ConnectionString,
    string? DatabaseName,
    string? CollectionName
);