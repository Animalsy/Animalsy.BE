namespace Animalsy.BE.Services.ContractorsAPI.Models.Dto
{
    public record UpdateContractorDto
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
    }
}
