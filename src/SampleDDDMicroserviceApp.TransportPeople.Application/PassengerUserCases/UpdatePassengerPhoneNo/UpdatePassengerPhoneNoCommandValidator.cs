using DNTPersianUtils.Core;
using FluentValidation;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.PassengerUserCases.UpdatePassengerPhoneNo;

public class UpdatePassengerPhoneNoCommandValidator : AbstractValidator<UpdatePassengerPhoneNoCommand>
{
    public UpdatePassengerPhoneNoCommandValidator()
    {
        RuleFor(x => x.NewPhoneNo)
            .NotEmpty().WithMessage("Phone number is required")
            .Must(newPhoneNo => newPhoneNo.IsValidIranianMobileNumber()).WithMessage("Phone number is invalid");
    }
}