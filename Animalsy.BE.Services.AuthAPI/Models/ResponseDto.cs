namespace Animalsy.BE.Services.AuthAPI.Models;

public record ResponseDto
{
    public bool IsSuccess { get; init; }
    public string Result { get; init; }
    public string Message { get; init; }
}