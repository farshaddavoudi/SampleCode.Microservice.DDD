using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Exceptions;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Extensions;

namespace SampleDDDMicroserviceApp.TransportPeople.Domain.PassengerAggregate.ValueObjects;

public record Address
{
    public decimal? Latitude { get; init; }
    public decimal? Longitude { get; init; }
    public string? FullAddress { get; set; }
    public string? Description { get; init; }

    private Address() { } //EF

    private Address(decimal? latitude, decimal? longitude, string? fullAddress, string? description)
    {
        Latitude = latitude;
        Longitude = longitude;
        FullAddress = fullAddress;
        Description = description;
    }

    public static Address Create(decimal? latitude, decimal? longitude, string? fullAddress, string? description)
    {
        if (latitude.HasValue is false && longitude.HasValue is false && fullAddress.IsNullOrWhitespace() && description.IsNullOrWhitespace())
        {
            throw new BusinessRuleException("An empty address is not valid");
        }

        if ((latitude.HasValue && longitude.HasValue is false) || (longitude.HasValue && latitude.HasValue is false))
        {
            throw new BusinessRuleException("Both the latitude and the longitude should be specified together");
        }

        if (latitude.HasValue && IsValidLatitude(latitude.Value) is false)
        {
            throw new BusinessRuleException("Latitude is not valid");
        }

        if (longitude.HasValue && IsValidLongitude(longitude.Value) is false)
        {
            throw new BusinessRuleException("Longitude is not valid");
        }

        return new Address(latitude, longitude, fullAddress, description);
    }

    private static bool IsValidLatitude(decimal latitude)
    {
        // Latitude should be between -90 and 90 degrees
        return latitude is >= -90 and <= 90;
    }

    private static bool IsValidLongitude(decimal longitude)
    {
        // Longitude should be between -180 and 180 degrees
        return longitude is >= -180 and <= 180;
    }
};