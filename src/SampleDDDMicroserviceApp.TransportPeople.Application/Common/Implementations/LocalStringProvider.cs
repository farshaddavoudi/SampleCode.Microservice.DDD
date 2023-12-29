using Microsoft.Extensions.Localization;
using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Exceptions;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Resources;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Resources.ExceptionMessages;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Resources.OtherMessages;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.Common.Implementations;

public class LocalStringProvider : ILocalStringProvider
{
    private readonly IStringLocalizer<MessageStrings> _messageStringsLocalizer;
    private readonly IStringLocalizer<ExceptionStrings> _exceptionStringsLocalizer;

    #region Constructor

    public LocalStringProvider(IStringLocalizer<MessageStrings> messageStringsLocalizer, IStringLocalizer<ExceptionStrings> exceptionStringsLocalizer)
    {
        _messageStringsLocalizer = messageStringsLocalizer;
        _exceptionStringsLocalizer = exceptionStringsLocalizer;
    }

    #endregion

    /// <summary>
    /// Get value from MessageStrings resource
    /// </summary>
    /// <param name="messageStringsKey"> Resource key </param>
    /// <returns></returns>
    public string? Message(string messageStringsKey)
    {
        return GetValueByKeyAndResource(messageStringsKey, StringResourceType.MessageStrings);
    }

    /// <summary>
    /// Get value from ExceptionStrings resource
    /// </summary>
    /// <param name="exceptionStringsKey"> Resource key </param>
    /// <returns></returns>
    public string Exception(string exceptionStringsKey)
    {
        return GetValueByKeyAndResource(exceptionStringsKey, StringResourceType.ExceptionStrings) ?? string.Empty;
    }

    private string? GetValueByKeyAndResource(string key, StringResourceType stringResourceType)
    {
        if (stringResourceType == StringResourceType.MessageStrings)
            return _messageStringsLocalizer.GetString(key);

        if (stringResourceType == StringResourceType.ExceptionStrings)
            return _exceptionStringsLocalizer.GetString(key);

        throw new BadRequestException("ResourceType is invalid");
    }
}