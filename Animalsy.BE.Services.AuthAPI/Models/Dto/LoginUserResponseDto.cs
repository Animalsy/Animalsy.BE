namespace Animalsy.BE.Services.AuthAPI.Models.Dto;

public record LoginUserResponseDto
{
    public bool IsSuccess { get; init; }
    public object Result { get; init; }
    public string Message { get; init; }
    public Guid UserId { get; init; }
    public string Token { get; init; }

}