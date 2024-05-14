namespace Animalsy.BE.Services.PetsAPI.Models.Dto
{
    public record UpdatePetDto
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Species { get; set; } = string.Empty;
        public string Race { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? ImageUrl { get; set; }
    }
}
