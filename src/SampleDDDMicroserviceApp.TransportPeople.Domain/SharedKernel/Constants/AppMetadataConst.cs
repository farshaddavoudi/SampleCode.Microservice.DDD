using System.Reflection;

namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Constants;

public static class AppMetadataConst
{
    public static readonly string AppVersion = Assembly.GetExecutingAssembly().GetName().Version!.ToString(3);

    public static readonly string EnglishFullName = "Sample DDD Microservice App";

    public static readonly string SolutionName = "SampleDDDMicroserviceApp.TransportPeople";
}