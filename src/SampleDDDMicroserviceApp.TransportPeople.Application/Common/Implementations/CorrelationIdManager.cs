using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.Common.Implementations;

public class CorrelationIdManager : ICorrelationIdManager
{
    private string _correlationId = Guid.NewGuid().ToString();

    public string Get()
    {
        return _correlationId;
    }

    public void Set(string correlationId)
    {
        _correlationId = correlationId;
    }
}