namespace Animalsy.BE.Services.VendorsAPI.Models.Dto
{
    public record CreateVendorDto
    {
        public string Name { get; set; } = string.Empty;
        public string Nip { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Building { get; set; } = string.Empty;
        public string? Flat { get; set; }
        public string PostalCode { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
    }
}
