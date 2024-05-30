namespace Animalsy.BE.Services.VisitAPI.Models.Dto;

public record CreateVisitDto
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public Guid VendorId { get; init; }
    public string ContractorName { get; init; }
    public Guid PetId { get; init; }
    public string CustomerName { get; init; }
    public string CustomerLastName { get; init; }
    public DateTime Date { get; init; }
    public string Comment { get; init; }
    public string State { get; init; }
}