using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Middlewares;

public class ExceptionHandlingMIddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMIddleware> _logger;

    public ExceptionHandlingMIddleware(RequestDelegate next, ILogger<ExceptionHandlingMIddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"Exception Occured: {exception.Message}");

            var exceptionDetails = GetExceptionDetails(exception);

            var problemDetails = new ProblemDetails
            {
                Status = exceptionDetails.Status,
                Type = exceptionDetails.Type,
                Title = exceptionDetails.Title,
                Detail = exceptionDetails.Detail
            };

            if(exceptionDetails.Errors is not null )
            {
                problemDetails.Extensions["errors"] = exceptionDetails.Errors;
            }

            context.Response.StatusCode = exceptionDetails.Status;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    private static ExceptionDetails GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
           Application.Exceptions.ValidationException validationException => new ExceptionDetails(
                StatusCodes.Status400BadRequest,
                "ValidationFailure",
                "Validation Error",
                "One or more validation errors has been occured",
                validationException.Errors
                ),
            _ => new ExceptionDetails(
                StatusCodes.Status500InternalServerError,
                "ServerError",
                "Server error",
                "An unexpected error has occured",
                null)
        };
    }

    internal record ExceptionDetails(
        int Status,
        string Type,
        string Title,
        string Detail,
        IEnumerable<object>? Errors
        );
}
