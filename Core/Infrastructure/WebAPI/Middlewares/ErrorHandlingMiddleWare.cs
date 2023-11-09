using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Project.Core.Domain;
using Project.Core.Infrastructure.SQLDB.Exceptions;
using System.Net;
using System.Text.Json;

namespace Project.Core.Infrastructure.WebAPI.Middlewares;

public class ErrorHandlingMiddleWare
{
    private readonly RequestDelegate next;

    public ErrorHandlingMiddleWare(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context, ILogger<ErrorHandlingMiddleWare> logger)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, logger, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, ILogger<ErrorHandlingMiddleWare> logger, Exception ex)
    {
        logger.LogError(message: ex.ToString());

        var code = HttpStatusCode.InternalServerError;
        if (ex is DomainException)
        {
            code = HttpStatusCode.BadRequest;
        }

        if (ex is OptimisticLockingException)
        {
            code = HttpStatusCode.Conflict;
        }

        string? result;
        if (ex is DomainException domainEx)
        {
            result = JsonSerializer.Serialize(new
            {
                errorCode = domainEx.ErrorCode,
                errorMessage = domainEx.Message,
                errorMessageKey = domainEx.ErrorMessageKey,
                errorMessageParam = domainEx.ErrorMessageParam
            });
        }
        else if (ex is SqlExecuteException)
        {
            result = JsonSerializer.Serialize(new
            {
                errorCode = "500",
                errorMessage = SqlExecuteException.CommonErrorMessage
            });
        }
        else
        {
            if (!string.IsNullOrEmpty(ex.StackTrace) && ex.StackTrace.Contains("SqlClient"))
            {
                result = JsonSerializer.Serialize(new
                {
                    errorCode = "500",
                    errorMessage = SqlExecuteException.CommonErrorMessage
                });
            }
            else
            {
                result = JsonSerializer.Serialize(new
                {
                    errorCode = "500",
                    errorMessage = ex.Message
                });
            }
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}