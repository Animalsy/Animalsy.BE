namespace Animalsy.BE.Services.CustomerAPI.Models.Dto;

public record ContractorDto
{
    public Guid Id { get; init; }
    public Guid VendorId { get; init; }
    public string Name { get; init; }
    public string LastName { get; init; }
    public string Specialization { get; init; }
    public string ImageUrl { get; init; }
}