using System.ComponentModel.DataAnnotations;

namespace Animalsy.BE.Services.ContractorsAPI.Models;

public class Contractor
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid VendorId { get; set; }

    [Required, MaxLength(20)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    public string LastName { get; set; } = string.Empty;

    [Required, MaxLength(400)]
    public string Specialization { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? ImageUrl { get; set; }
}