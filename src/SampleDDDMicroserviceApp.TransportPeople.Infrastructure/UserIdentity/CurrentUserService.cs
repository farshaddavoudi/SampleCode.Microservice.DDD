﻿using Microsoft.AspNetCore.Http;
using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.ConfigurationSettings;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Constants;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Extensions;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.User;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Utilities;
using System.Security.Claims;

namespace SampleDDDMicroserviceApp.TransportPeople.Infrastructure.UserIdentity;

public class CurrentUserService(
    IHttpContextAccessor httpContextAccessor,
    CryptoUtility cryptoUtility,
    AppSettings appSettings
) : ICurrentUserService
{
    public MiniUser? User()
    {
        if (IsAuthenticated() is false)
            return null;

        return new MiniUser
        {
            Id = GetUserIdFromToken(),
            FullName = GetClaimValueFromJwtToken(ClaimTypes.Name),
            PersonnelCode = GetClaimValueFromJwtToken(JwtTokenClaimConst.PersonnelCode).ToInt(),
            UnitName = GetClaimValueFromJwtToken(JwtTokenClaimConst.UnitName),
            PostTitle = GetClaimValueFromJwtToken(JwtTokenClaimConst.PostTitle),
            BoxId = GetClaimValueFromJwtToken(JwtTokenClaimConst.BoxId).ToInt(true),
            Roles = GetRolesFromToken(),
            WorkLocationCode = GetClaimValueFromJwtToken(JwtTokenClaimConst.WorkLocationCode).ToInt(true)
        };
    }

    public bool IsAuthenticated()
    {
        return httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
    }

    public bool IsAdmin()
    {
        return GetRolesFromToken().Contains(RoleConst.Identity_Administrator);
    }

    #region Private Methods

    private int GetUserIdFromToken()
    {
        if (IsAuthenticated() is false)
            throw new UnauthorizedAccessException("Current user is not logged in");

        var encryptedUserId = httpContextAccessor.HttpContext!.User.Claims
            .First(i => i.Type == ClaimTypes.NameIdentifier).Value;

        if (string.IsNullOrWhiteSpace(encryptedUserId))
            throw new UnauthorizedAccessException("Invalid user");

        var userId = cryptoUtility.FromEncryptedBase64String(
            encryptedUserId,
            appSettings.AuthSettings!.UserIdEncryptionKey);

        return userId.ToInt();
    }

    private List<string> GetRolesFromToken()
    {
        var stringRoles = GetClaimValueFromJwtToken(ClaimTypes.Role);

        return stringRoles.Split(",").ToList() ?? new List<string>();
    }

    private string GetClaimValueFromJwtToken(string claimType)
    {
        var claimValue = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(i => i.Type == claimType)?.Value;

        return claimValue ?? string.Empty;
    }
}

#endregion