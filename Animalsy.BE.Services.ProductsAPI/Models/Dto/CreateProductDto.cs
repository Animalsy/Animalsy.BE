﻿namespace Animalsy.BE.Services.ProductAPI.Models.Dto;

public record CreateProductDto
{
    public Guid VendorId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string CategoryAndSubCategory { get; init; }
    public decimal MinPrice { get; init; }
    public decimal MaxPrice { get; init; }
    public decimal PromoPrice { get; init; }
    public TimeSpan Duration { get; init; }
}