namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string msg) : base(msg)
    { }
}