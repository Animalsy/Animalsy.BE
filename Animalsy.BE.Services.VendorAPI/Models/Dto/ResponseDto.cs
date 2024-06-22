namespace Animalsy.BE.Services.VendorAPI.Models.Dto;

public record ResponseDto
{
    public bool IsSuccess { get; init; }
    public object Result { get; init; }
    public string Message { get; init; }
}