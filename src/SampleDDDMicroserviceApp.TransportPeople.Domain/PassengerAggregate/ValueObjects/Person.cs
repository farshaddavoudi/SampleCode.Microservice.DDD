using DNTPersianUtils.Core;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Exceptions;

namespace SampleDDDMicroserviceApp.TransportPeople.Domain.PassengerAggregate.ValueObjects;

public record Person
{
    public const int FullNameMaxLength = 200;

    public int? UserId { get; init; }
    public string? FullName { get; init; }
    public string? PhoneNo { get; init; }
    public string? NationalCode { get; init; }

    private Person() { } //EF

    private Person(int? userId, string? fullName, string? phoneNo, string? nationalCode)
    {
        UserId = userId;
        FullName = fullName;
        PhoneNo = phoneNo;
        NationalCode = nationalCode;
    }

    public static Person Create(int? userId, string? fullName, string? phoneNo, string? nationalCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fullName);

        if (fullName.Length > FullNameMaxLength)
        {
            throw new BusinessRuleException("Passenger fullname cannot be bigger than 200 characters");
        }

        if (string.IsNullOrWhiteSpace(phoneNo) is false && phoneNo.IsValidIranianMobileNumber() is false)
        {
            throw new BusinessRuleException("Passenger phone number is invalid");
        }

        if (string.IsNullOrWhiteSpace(nationalCode) is false && nationalCode.IsValidIranianNationalCode() is false)
        {
            throw new BusinessRuleException("Passenger national code in invalid");
        }

        var personInfo = new Person(userId, fullName, phoneNo, nationalCode);

        return personInfo;
    }
}