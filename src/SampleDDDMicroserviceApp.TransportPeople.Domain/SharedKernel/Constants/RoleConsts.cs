// ReSharper disable InconsistentNaming
namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Constants;

public class RoleConst //* Do not change Role values *//
{
    public const string Identity_Administrator = nameof(Identity_Administrator);
}

public record Administrator; //To use nameof