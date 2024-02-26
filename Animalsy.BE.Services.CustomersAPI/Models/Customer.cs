using System.ComponentModel.DataAnnotations;

namespace Animalsy.BE.Services.CustomersAPI.Models
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid VendorId { get; set; }
        [Required, MaxLength(20)]
        public string Name { get; set; }
        [Required, MaxLength(20)]
        public string LastName { get; set; }
        [Required, MaxLength(20)]
        public string City { get; set; }
        [MaxLength(40)]
        public string Street { get; set; }
        [MaxLength(6)]
        public string PostalCode { get; set; }
        [MaxLength(10)]
        public string Building { get; set; }
        [MaxLength(10)]
        public string Flat { get; set; }
        [MaxLength(9)]
        public string PhoneNumber { get; set; }
        [Required, MaxLength(50)]
        public string EmailAddress { get; set; }

    }
}
