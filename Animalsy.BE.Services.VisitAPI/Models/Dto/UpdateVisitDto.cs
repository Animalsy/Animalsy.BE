namespace Animalsy.BE.Services.VisitAPI.Models.Dto;

public record UpdateVisitDto
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public string Comment { get; set; }
    public string Status { get; set; }
    public DateTime Date { get; set; }
}