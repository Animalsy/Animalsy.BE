using Animalsy.BE.Services.VendorAPI.Models.Dto;

namespace Animalsy.BE.Services.VendorAPI.Repository;

public interface IVendorRepository
{
    Task<IEnumerable<VendorDto>> GetAllAsync();
    Task<IEnumerable<VendorDto>> GetByNameAsync(string name);
    Task<VendorDto> GetByIdAsync(Guid vendorId);
    Task<VendorDto> GetByEmailAsync (string email);
    Task<VendorProfileDto> GetVendorProfileAsync(Guid vendorId);
    Task<Guid> CreateAsync(CreateVendorDto vendorDto);
    Task<bool> TryUpdateAsync(UpdateVendorDto vendorDto);
    Task<bool> TryDeleteAsync(Guid vendorId);
}