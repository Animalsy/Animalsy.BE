namespace Animalsy.BE.Services.VendorAPI.Models.Dto;

public record VendorProfileDto
{
    public VendorDto Vendor { get; init; }
    public IEnumerable<ContractorDto> Contractors { get; init; }
    public IEnumerable<VisitDto> Visits { get; init; }
    public IEnumerable<KeyValuePair<string, string>> ResponseDetails { get; init; }
}