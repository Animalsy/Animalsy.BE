using Animalsy.BE.Services.VendorsAPI.Models.Dto;

namespace Animalsy.BE.Services.VendorsAPI.Repository;

public interface IVendorRepository
{
    Task<IEnumerable<VendorResponseDto>> GetAllAsync();
    Task<IEnumerable<VendorResponseDto>> GetByNameAsync(string name);
    Task<VendorResponseDto?> GetByIdAsync(Guid vendorId);
    Task<VendorResponseDto?> GetByEmailAsync (string email);
    Task<Guid> CreateAsync(CreateVendorDto vendorDto);
    Task<bool> TryUpdateAsync(UpdateVendorDto vendorDto);
    Task<bool> TryDeleteAsync(Guid vendorId);
}