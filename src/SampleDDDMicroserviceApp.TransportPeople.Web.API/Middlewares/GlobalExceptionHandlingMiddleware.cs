using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Exceptions;
using System.Net;
using System.Text.Json;
using UnauthorizedAccessException = SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Exceptions.UnauthorizedAccessException;

namespace SampleDDDMicroserviceApp.TransportPeople.Web.API.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
    private readonly ICorrelationIdManager _correlationIdManager;

    #region Constructor

    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger, ICorrelationIdManager correlationIdManager)
    {
        _logger = logger;
        _correlationIdManager = correlationIdManager;
    }

    #endregion

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            // It will handle all exceptions thrown in the application
            // It is global so it can be from DB error, service error or controller error 
            var exceptionResult = await HandleExceptionAsync(context, ex);

            _logger.LogError(ex, "{exceptionResult}", exceptionResult);
        }
    }

    public async Task<string> HandleExceptionAsync(HttpContext context, Exception ex)
    {
        HttpStatusCode statusCode;
        string message;
        string? stackTrace;

        var exceptionType = ex.GetType();

        if (exceptionType == typeof(NotFoundException))
        {
            message = ex.Message;
            statusCode = HttpStatusCode.NotFound;
            stackTrace = ex.StackTrace;
        }
        else if (exceptionType == typeof(BadRequestException))
        {
            message = ex.Message;
            statusCode = HttpStatusCode.BadRequest;
            stackTrace = ex.StackTrace;
        }
        else if (exceptionType == typeof(BusinessRuleException))
        {
            message = ex.Message;
            statusCode = HttpStatusCode.UnprocessableEntity;
            stackTrace = ex.StackTrace;
        }
        else if (exceptionType == typeof(UnauthorizedAccessException))
        {
            message = ex.Message;
            statusCode = HttpStatusCode.Unauthorized;
            stackTrace = ex.StackTrace;
        }
        else
        {
            message = "عملیات با خطا همراه شد"; //ex.Message;
            statusCode = HttpStatusCode.InternalServerError;
            stackTrace = ex.StackTrace;
        }

        var error = new Error
        {
#if DEBUG
            ExceptionMessage = ex.Message,
            StackTrace = stackTrace
#endif
        };

        var exceptionResult = JsonSerializer.Serialize(error);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsync(exceptionResult);

        return exceptionResult;
    }
}

public class Error
{
#if DEBUG
    public string? ExceptionMessage { get; set; }
    public string? StackTrace { get; set; }
#endif
}