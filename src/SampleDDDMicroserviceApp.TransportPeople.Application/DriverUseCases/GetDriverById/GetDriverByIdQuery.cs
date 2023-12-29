using SampleDDDMicroserviceApp.TransportPeople.Domain.DriverAggregate;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Exceptions;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.DriverUseCases.GetDriverById;

public record GetDriverByIdQuery(Guid DriverId) : IRequest<GetDriverByIdQueryResponse>;

public class GetDriverByIdQueryHandler(IReadOnlyRepository<Driver> driverRepository) : IRequestHandler<GetDriverByIdQuery, GetDriverByIdQueryResponse>
{
    public async Task<GetDriverByIdQueryResponse> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken)
    {
        var driver = await driverRepository.GetByIdAsync(new DriverId(request.DriverId), cancellationToken);
        if (driver is null)
        {
            throw new BadRequestException("No driver was found");
        }

        return new GetDriverByIdQueryResponse(driver.UserId, driver.DriverType, driver.DriverStatus);
    }
}