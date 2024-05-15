using Animalsy.BE.Services.ContractorsAPI.Models.Dto;

namespace Animalsy.BE.Services.ContractorsAPI.Repository;

public interface IContractorRepository
{
    Task<Guid> CreateAsync(CreateContractorDto contractorDto);
    Task<ContractorResponseDto?> GetByIdAsync(Guid contractorId);
    Task<IEnumerable<ContractorResponseDto>> GetByVendorAsync(Guid vendorId);
    Task<bool> TryUpdateAsync(UpdateContractorDto contractorDto);
    Task<bool> TryDeleteAsync(Guid contractorId);
}