namespace Animalsy.BE.Services.VisitAPI.Models.Dto;

public record UpdateVisitDto
{
    public Guid Id { get; init; }
    public string Comment { get; set; }
    public string State { get; set; }
    public DateTime Date { get; set; }
}