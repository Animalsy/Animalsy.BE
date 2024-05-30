namespace Animalsy.BE.Services.ContractorAPI.Models.Dto;

public record ContractorResponseDto
{
    public Guid Id { get; init; }
    public Guid VendorId { get; init; }
    public string Name { get; init; }
    public string LastName { get; init; }
    public string Specialization { get; init; }
    public string ImageUrl { get; init; }
}