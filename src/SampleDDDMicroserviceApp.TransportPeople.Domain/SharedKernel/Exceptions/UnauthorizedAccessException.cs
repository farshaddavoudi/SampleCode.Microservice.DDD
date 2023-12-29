namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Exceptions;

public class UnauthorizedAccessException : Exception
{
    public UnauthorizedAccessException(string msg) : base(msg)
    { }
}