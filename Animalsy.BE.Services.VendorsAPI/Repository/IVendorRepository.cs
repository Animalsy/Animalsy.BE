using Animalsy.BE.Services.VendorAPI.Models.Dto;

namespace Animalsy.BE.Services.VendorAPI.Repository;

public interface IVendorRepository
{
    Task<IEnumerable<VendorResponseDto>> GetAllAsync();
    Task<IEnumerable<VendorResponseDto>> GetByNameAsync(string name);
    Task<VendorResponseDto> GetByIdAsync(Guid vendorId);
    Task<VendorResponseDto> GetByEmailAsync (string email);
    Task<Guid> CreateAsync(CreateVendorDto vendorDto);
    Task<bool> TryUpdateAsync(UpdateVendorDto vendorDto);
    Task<bool> TryDeleteAsync(Guid vendorId);
}