namespace Animalsy.BE.Services.AuthAPI.Models.Dto;

public record RegisterUserResponseDto
{
    public Guid UserId { get; set; }
    public Guid CustomerId { get; set; }
}