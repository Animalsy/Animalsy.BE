using System.ComponentModel.DataAnnotations;

namespace Animalsy.BE.Services.VendorAPI.Models;

public record Vendor
{
    [Key]
    public Guid Id { get; init; }

    [Required]
    public Guid UserId { get; init; }

    [Required, MaxLength(50)] 
    public string Name { get; set; }

    [Required, MaxLength(10)]
    public string Nip { get; set; }

    [Required, MaxLength(20)]
    public string City { get; set; }

    [MaxLength(40)]
    public string Street { get; set; }

    [MaxLength(5)]
    public string Building { get; set; }

    [MaxLength(5)]
    public string Flat { get; set; }

    [MaxLength(6)]
    public string PostalCode { get; set; }

    [MaxLength(9)]
    public string PhoneNumber { get; set; }

    [Required, MaxLength(50)]
    public string EmailAddress { get; set; }

    [Required]
    public TimeOnly OpeningHour { get; set; }

    [Required]
    public TimeOnly ClosingHour { get; set; }
}