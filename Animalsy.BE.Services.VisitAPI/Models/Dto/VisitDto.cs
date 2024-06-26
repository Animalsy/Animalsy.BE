﻿namespace Animalsy.BE.Services.VisitAPI.Models.Dto;

public record VisitDto
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public Guid VendorId { get; init; }
    public Guid ContractorId { get; init; }
    public Guid CustomerId { get; init; }
    public Guid PetId { get; init; }
    public DateTime Date { get; init; }
    public string Comment { get; init; }
    public string Status { get; init; }
}