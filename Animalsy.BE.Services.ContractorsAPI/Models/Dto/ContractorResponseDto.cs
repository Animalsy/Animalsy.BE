namespace Animalsy.BE.Services.ContractorsAPI.Models.Dto
{
    public record ContractorResponseDto
    {
        public Guid Id { get; set; } = Guid.Empty;
        public Guid VendorId { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
    }
}
