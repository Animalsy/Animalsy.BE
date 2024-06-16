namespace Animalsy.BE.Services.VisitAPI.Models.Dto;

public class VisitResponseDto
{
    public Guid Id { get; set; }
    public ProductDto Product { get; set; }
    public VendorDto Vendor { get; set; }
    public ContractorDto Contractor { get; set; }
    public PetDto Pet { get; set; }
    public CustomerDto Customer { get; set; }
    public DateTime Date { get; set; }
    public string Comment { get; set; }
    public string State { get; set; }
    public IEnumerable<KeyValuePair<string, string>> ResponseDetails { get; init; }
}