namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Exceptions;

public class BusinessRuleException : Exception
{
    public BusinessRuleException(string msg) : base(msg)
    {
    }
}