using System.Text.Json;

namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Extensions;

public static class ObjectExtensions
{
    public static T Clone<T>(this object value)
    {
        return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(value)) ?? throw new InvalidOperationException();
    }
}