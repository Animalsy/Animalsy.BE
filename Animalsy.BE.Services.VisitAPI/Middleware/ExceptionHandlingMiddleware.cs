using System.Net;

namespace Animalsy.BE.Services.VisitAPI.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IWebHostEnvironment _environment;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger,
        IWebHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occurred");
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var errorDetails = new ErrorDetails
        {
            StatusCode = context.Response.StatusCode,
            Message = "Internal Server Error"
        };

        if (_environment.IsDevelopment())
        {
            errorDetails.StackTrace = exception.StackTrace;
        }

        return context.Response.WriteAsJsonAsync(errorDetails);
    }
}