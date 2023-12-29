using Microsoft.EntityFrameworkCore;
using SampleDDDMicroserviceApp.TransportPeople.Domain.DriverAggregate;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Extensions;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.User;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.DriverUseCases.GetAllDriversByGridFilters;

public record GetAllDriversByGridFiltersQuery(string? SearchTerm) : IRequest<List<GridDriverDto>>;

public class GetAllDriversByFiltersQueryHandler(
    IReadOnlyRepository<Driver> driverRepository,
    IReadOnlyRepository<User> userRepository
    ) : IRequestHandler<GetAllDriversByGridFiltersQuery, List<GridDriverDto>>
{
    public async Task<List<GridDriverDto>> Handle(GetAllDriversByGridFiltersQuery request, CancellationToken cancellationToken)
    {
        var query =
            from driver in driverRepository.AsQueryable()
            join user in userRepository.AsQueryable()
                on driver.UserId equals user.UserId

            where request.SearchTerm.IsNullOrWhitespace() ||
                  user.FullName!.Contains(request.SearchTerm) ||
                  user.PersonnelCode.ToString().Contains(request.SearchTerm)

            select new GridDriverDto(
                driver.Id!.Value,
                user.FullName,
                driver.DriverType,
                driver.DriverType.ToDisplayName(true),
                driver.DriverStatus,
                driver.DriverStatus.ToDisplayName(true));

        return await query.ToListAsync(cancellationToken);
    }
}