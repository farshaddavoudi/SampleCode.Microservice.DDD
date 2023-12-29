namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string msg) : base(msg)
    { }
}