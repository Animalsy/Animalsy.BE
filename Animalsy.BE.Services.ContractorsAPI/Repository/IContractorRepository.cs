using Animalsy.BE.Services.ContractorAPI.Models.Dto;

namespace Animalsy.BE.Services.ContractorAPI.Repository;

public interface IContractorRepository
{
    Task<Guid> CreateAsync(CreateContractorDto contractorDto);
    Task<ContractorResponseDto> GetByIdAsync(Guid contractorId);
    Task<IEnumerable<ContractorResponseDto>> GetByVendorAsync(Guid vendorId);
    Task<bool> TryUpdateAsync(UpdateContractorDto contractorDto);
    Task<bool> TryDeleteAsync(Guid contractorId);
}