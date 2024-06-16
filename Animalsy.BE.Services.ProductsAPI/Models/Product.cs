using System.ComponentModel.DataAnnotations;

namespace Animalsy.BE.Services.ProductAPI.Models;

public record Product
{
    [Key]
    public Guid Id { get; init; }
    [Required]
    public Guid VendorId { get; init; }

    [Required, MaxLength(50)] 
    public string Name { get; set; }

    [Required, MaxLength(1000)]
    public string Description { get; set; }

    [Required, MaxLength(60)]
    public string CategoryAndSubCategory { get; set; }

    [Required]
    public decimal MinPrice { get; set; }

    public decimal MaxPrice { get; set; }

    public decimal PromoPrice { get; set; }

    [Required]
    public TimeSpan Duration { get; set; }
}