using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Extensions;

namespace SampleDDDMicroserviceApp.TransportPeople.Infrastructure.UserIdentity;

public class JwtTokenClaimConst
{
    public static string PersonnelCode = nameof(PersonnelCode).ToLowerFirstChar();
    public static string UnitName = nameof(UnitName).ToLowerFirstChar();
    public static string PostTitle = nameof(PostTitle).ToLowerFirstChar();
    public static string BoxId = nameof(BoxId).ToLowerFirstChar();
    public static string WorkLocationCode = nameof(WorkLocationCode).ToLowerFirstChar();
}