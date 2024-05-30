using System.ComponentModel.DataAnnotations;

namespace Animalsy.BE.Services.VisitAPI.Models;

public record Visit
{
    [Key]
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public Guid VendorId { get; init; }
    public Guid ContractorId { get; init; }
    public Guid PetId { get; init; }
    public Guid CustomerId { get; init; }
    public DateTime Date { get; set; }
    [MaxLength(100)]
    public string Comment { get; set; }
    [MaxLength(20)]
    public string State { get; set; }
}