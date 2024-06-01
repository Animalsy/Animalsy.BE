using System.ComponentModel.DataAnnotations;

namespace Animalsy.BE.Services.VisitAPI.Models;

public record Visit
{
    [Key]
    public Guid Id { get; init; }

    [Required]
    public Guid ProductId { get; init; }

    [Required]
    public Guid VendorId { get; init; }

    [Required]
    public Guid ContractorId { get; init; }

    [Required]
    public Guid PetId { get; init; }

    [Required]
    public Guid CustomerId { get; init; }

    [Required]
    public DateTime Date { get; set; }

    [MaxLength(100)]
    public string Comment { get; set; }
    [MaxLength(20)]
    public string State { get; set; }
}