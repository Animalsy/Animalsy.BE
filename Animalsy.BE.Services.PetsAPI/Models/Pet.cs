using System.ComponentModel.DataAnnotations;

namespace Animalsy.BE.Services.PetAPI.Models;

public record Pet
{
    [Key]
    public Guid Id { get; init; }

    [Required]
    public Guid CustomerId { get; init; }

    [Required, MinLength(1), MaxLength(20)]
    public string Species { get; set; }

    [Required, MinLength(1), MaxLength(40)]
    public string Race { get; set; }

    [Required, MinLength(1), MaxLength(20)]
    public string Name { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [MinLength(1), MaxLength(500)]
    public string ImageUrl { get; set; }
}