using Animalsy.BE.Services.ContractorAPI.Models.Dto;

namespace Animalsy.BE.Services.ContractorAPI.Repository;

public interface IContractorRepository
{
    Task<Guid> CreateAsync(CreateContractorDto createContractorDto);
    Task<ContractorDto> GetByIdAsync(Guid contractorId);
    Task<IEnumerable<ContractorDto>> GetByVendorAsync(Guid vendorId, string specialization = null);
    Task<bool> TryUpdateAsync(UpdateContractorDto updateContractorDto);
    Task<bool> TryDeleteAsync(Guid contractorId);
}