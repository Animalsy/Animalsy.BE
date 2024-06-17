using System.ComponentModel.DataAnnotations;

namespace Animalsy.BE.Services.ContractorAPI.Models;

public record Contractor
{
    [Key]
    public Guid Id { get; init; }

    [Required]
    public Guid UserId { get; init; }

    [Required]
    public Guid VendorId { get; init; }

    [Required, MaxLength(20)]
    public string Name { get; set; }

    [Required, MaxLength(20)]
    public string LastName { get; set; }

    [Required, MaxLength(60)]
    public string Specialization { get; set; }

    [MaxLength(500)]
    public string? ImageUrl { get; set; }
}