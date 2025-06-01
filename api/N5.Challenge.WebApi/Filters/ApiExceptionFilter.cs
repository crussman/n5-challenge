using FluentValidation;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Net;

namespace N5.Challenge.WebApi.Filters;

public class ApiExceptionFilter(ILogger<ApiExceptionFilter> logger) : IExceptionFilter
{
    private readonly ILogger<ApiExceptionFilter> _logger = logger;

    public void OnException(ExceptionContext context)
    {
        int statusCode;
        IEnumerable<string> errors;

        switch (context.Exception)
        {
            case KeyNotFoundException ex:
                statusCode = (int)HttpStatusCode.NotFound;
                errors = [ex.Message];

                break;

            case ValidationException validationEx:
                statusCode = (int)HttpStatusCode.BadRequest;
                errors = [.. validationEx.Errors.Select(e => e.ErrorMessage)];

                break;

            default:
                statusCode = (int)HttpStatusCode.InternalServerError;
                errors = ["An unexpected error occurred."];

                _logger.LogError(context.Exception, "Unhandled exception");

                break;
        }

        context.Result = new ObjectResult(new { errors })
        {
            StatusCode = statusCode
        };

        context.ExceptionHandled = true;
    }
}