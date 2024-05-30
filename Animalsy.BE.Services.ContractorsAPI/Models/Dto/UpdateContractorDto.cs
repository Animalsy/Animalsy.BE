namespace Animalsy.BE.Services.ContractorAPI.Models.Dto;

public record UpdateContractorDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string LastName { get; init; }
    public string Specialization { get; init; }
    public string ImageUrl { get; init; }
}