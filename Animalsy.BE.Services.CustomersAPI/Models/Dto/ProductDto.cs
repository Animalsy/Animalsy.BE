namespace Animalsy.BE.Services.CustomerAPI.Models.Dto;

public record ProductDto
{
    public Guid Id { get; init; }
    public Guid VendorId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string CategoryAndSubCategory { get; init; }
    public decimal MinPrice { get; init; }
    public decimal MaxPrice { get; init; }
    public decimal PromoPrice { get; init; }
    public TimeSpan Duration { get; init; }
}