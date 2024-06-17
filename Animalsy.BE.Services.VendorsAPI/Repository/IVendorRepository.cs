using Animalsy.BE.Services.VendorAPI.Models.Dto;

namespace Animalsy.BE.Services.VendorAPI.Repository;

public interface IVendorRepository
{
    Task<IEnumerable<VendorDto>> GetAllAsync();
    Task<VendorDto> GetByIdAsync(Guid vendorId);
    Task<IEnumerable<VendorProfileDto>> GetVendorProfilesAsync(Guid userId);
    Task<Guid> CreateAsync(CreateVendorDto createVendorDto);
    Task<bool> TryUpdateAsync(UpdateVendorDto updateVendorDto);
    Task<bool> TryDeleteAsync(Guid vendorId);
}