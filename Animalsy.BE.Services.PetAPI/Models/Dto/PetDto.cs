namespace Animalsy.BE.Services.PetsAPI.Models.Dto
{
    public class PetDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Species { get; set; }
        public string Race { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImageUrl { get; set; }
    }
}
