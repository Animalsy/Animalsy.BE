namespace Animalsy.BE.Services.AuthAPI.Models.Dto;

public record LoginUserResponseDto
{
    public Guid UserId { get; init; }
    public string Token { get; init; }

}