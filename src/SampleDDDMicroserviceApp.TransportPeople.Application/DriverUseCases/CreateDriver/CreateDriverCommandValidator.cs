using FluentValidation;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.DriverUseCases.CreateDriver;

public class CreateDriverCommandValidator : AbstractValidator<CreateDriverCommand>
{
    public CreateDriverCommandValidator()
    {
        RuleFor(d => d.DriverType)
            .NotEmpty().WithMessage("Driver type is not selected");

        RuleFor(d => d.UserId)
            .NotEmpty().WithMessage("User is not selected");
    }
}