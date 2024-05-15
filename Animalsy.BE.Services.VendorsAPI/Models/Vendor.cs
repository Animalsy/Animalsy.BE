using System.ComponentModel.DataAnnotations;

namespace Animalsy.BE.Services.VendorsAPI.Models;

public class Vendor //TODO: Localization
{
    [Key]
    public Guid Id { get; set; }

    [Required, MaxLength(50)] 
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(10)]
    public string Nip { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    public string City { get; set; } = string.Empty;

    [MaxLength(40)]
    public string Street { get; set; } = string.Empty;

    [MaxLength(5)]
    public string Building { get; set; } = string.Empty;

    [MaxLength(5)]
    public string? Flat { get; set; }

    [MaxLength(6)]
    public string PostalCode { get; set; } = string.Empty;

    [MaxLength(9)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string EmailAddress { get; set; } = string.Empty;
}