using System.ComponentModel.DataAnnotations;

namespace Animalsy.BE.Services.PetAPI.Models
{
    public class Pet
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid CustomerId { get; set; }
        [Required]
        public string  Species { get; set; }
        [Required]
        public string Race { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public string ImageUrl { get; set; }
    }
}
