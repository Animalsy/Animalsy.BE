namespace Animalsy.BE.Services.PetsAPI.Models.Dto
{
    public class UpdatePetDto
    {
        public string Species { get; set; }
        public string Race { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImageUrl { get; set; }
    }
}
