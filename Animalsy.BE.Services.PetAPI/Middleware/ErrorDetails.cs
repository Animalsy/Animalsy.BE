namespace Animalsy.BE.Services.PetAPI.Middleware;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string StackTrace { get; set; }
}