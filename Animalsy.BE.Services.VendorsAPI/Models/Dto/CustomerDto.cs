namespace Animalsy.BE.Services.VendorAPI.Models.Dto;

public record CustomerDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string LastName { get; init; }
    public string City { get; init; }
    public string Street { get; init; }
    public string Building { get; init; }
    public string Flat { get; init; }
    public string PostalCode { get; init; }
    public string PhoneNumber { get; init; }
    public string EmailAddress { get; init; }
}