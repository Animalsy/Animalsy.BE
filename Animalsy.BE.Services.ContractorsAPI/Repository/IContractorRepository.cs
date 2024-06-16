using Animalsy.BE.Services.ContractorAPI.Models.Dto;

namespace Animalsy.BE.Services.ContractorAPI.Repository;

public interface IContractorRepository
{
    Task<Guid> CreateAsync(CreateContractorDto createContractorDto);
    Task<ContractorDto> GetByIdAsync(Guid contractorId);
    Task<IEnumerable<ContractorDto>> GetByVendorAsync(Guid vendorId);
    Task<bool> TryUpdateAsync(UpdateContractorDto updateContractorDto);
    Task DeleteAsync(ContractorDto contractorDto);
}