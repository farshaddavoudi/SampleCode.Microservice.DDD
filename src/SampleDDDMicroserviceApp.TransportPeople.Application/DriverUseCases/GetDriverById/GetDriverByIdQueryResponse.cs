using SampleDDDMicroserviceApp.TransportPeople.Domain.DriverAggregate.Enums;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.DriverUseCases.GetDriverById;

public record GetDriverByIdQueryResponse(
    int UserId,
    DriverType DriverType,
    DriverStatus DriverStatus);