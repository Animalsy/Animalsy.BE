namespace Animalsy.BE.Services.ProductAPI.Models.Dto;

public record ProductResponseDto
{
    public Guid Id { get; init; }
    public Guid VendorId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string Category { get; init; }
    public string SubCategory { get; init; }
    public decimal MinPrice { get; init; }
    public decimal MaxPrice { get; init; }
    public decimal PromoPrice { get; init; }
    public TimeSpan Duration { get; init; }
}