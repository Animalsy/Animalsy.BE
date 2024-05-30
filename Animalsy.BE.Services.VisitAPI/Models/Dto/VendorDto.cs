namespace Animalsy.BE.Services.VisitAPI.Models.Dto;

public record VendorDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Nip { get; init; }
    public string City { get; init; }
    public string Street { get; init; }
    public string Building { get; init; }
    public string? Flat { get; init; }
    public string PostalCode { get; init; }
    public string PhoneNumber { get; init; }
    public string EmailAddress { get; init; }
    public TimeOnly OpeningHour { get; init; }
    public TimeOnly ClosingHour { get; init; }
}