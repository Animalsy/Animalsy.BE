namespace Animalsy.BE.Services.CustomersAPI.Models.Dto
{
    public record CreateCustomerDto
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? Building { get; set; }
        public string? Flat { get; set; }
        public string? PostalCode { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
    }
}
