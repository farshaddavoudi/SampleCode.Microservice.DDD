using SampleDDDMicroserviceApp.TransportPeople.Domain.DriverAggregate.Enums;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.DriverUseCases.GetAllDriversByGridFilters;

public record GridDriverDto(
    Guid DriverId,
    string? DriverName,
    DriverType DriverType,
    string? DriverTypeDisplay,
    DriverStatus DriverStatus,
    string? DriverStatusDisplay);