namespace Animalsy.BE.Services.VisitAPI.Configuration;

public record ServiceUrlConfiguration
{
    public string CustomerApi { get; init; }
    public string PetApi { get; init; }
    public string VendorApi { get; init; }
}