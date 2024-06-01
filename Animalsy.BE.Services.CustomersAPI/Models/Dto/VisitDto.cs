namespace Animalsy.BE.Services.CustomerAPI.Models.Dto;

public record VisitDto
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
}