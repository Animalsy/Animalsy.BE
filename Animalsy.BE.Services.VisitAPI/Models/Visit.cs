using System.ComponentModel.DataAnnotations;

namespace Animalsy.BE.Services.VisitAPI.Models;

public class Visit
{
    [Key]
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid VendorId { get; set; }
    public Guid ContractorId { get; set; }
    public Guid PetId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime Date { get; set; }
    public string Comment { get; set; }
    public string State { get; set; }

}