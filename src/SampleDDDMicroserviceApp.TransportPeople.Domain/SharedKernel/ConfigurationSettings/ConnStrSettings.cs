namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;

public record ConnStrSettings(string? AppDbConnStr, string? HangfireConnStr);