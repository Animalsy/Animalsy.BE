namespace Animalsy.BE.Services.CustomerAPI.Models.Dto;

public record CustomerProfileDto
{
    public CustomerDto Customer { get; init; }
    public IEnumerable<PetDto> Pets { get; init; }
    public IEnumerable<VisitDto> Visits { get; init; }
    public IEnumerable<KeyValuePair<string, string>> ResponseDetails { get; init; }
}