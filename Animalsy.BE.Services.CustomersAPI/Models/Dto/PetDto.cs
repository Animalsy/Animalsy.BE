namespace Animalsy.BE.Services.CustomerAPI.Models.Dto
{
    public record PetDto
    {
        public Guid Id { get; init; }
        public Guid CustomerId { get; init; }
        public string Species { get; init; }
        public string Race { get; init; }
        public string Name { get; init; }
        public DateTime DateOfBirth { get; init; }
        public string ImageUrl { get; init; }
    }
}
