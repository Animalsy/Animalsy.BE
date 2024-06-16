namespace Animalsy.BE.Services.AuthAPI.Middleware;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string StackTrace { get; set; }
}