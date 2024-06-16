using System.Net;

namespace Animalsy.BE.Services.AuthAPI.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IWebHostEnvironment _environment;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger,
        IWebHostEnvironment environment)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _environment = environment ?? throw new ArgumentNullException(nameof(environment));
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

        var errorDetails = new ErrorDetails();

        if (exception is UnauthorizedAccessException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            errorDetails.StatusCode = context.Response.StatusCode;
            errorDetails.Message = "Unauthorized";
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            errorDetails.StatusCode = context.Response.StatusCode;
            errorDetails.Message = "Internal Server Error";

            if (_environment.IsDevelopment())
            {
                errorDetails.StackTrace = exception.StackTrace;
            }
        }

        return context.Response.WriteAsJsonAsync(errorDetails);
    }
}