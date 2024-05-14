using Animalsy.BE.Services.ContractorsAPI.Models.Dto;

namespace Animalsy.BE.Services.ContractorsAPI.Repository
{
    public class ContractorRepository : IContractorRepository
    {
        public Task<ContractorResponseDto?> GetByIdAsync(Guid contractorId)
        {
            throw new NotImplementedException();
        }

        public Task<ContractorResponseDto?> GetByVendorAsync(Guid vendorId)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateAsync(CreateContractorDto contractorDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(UpdateContractorDto customerDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryDeleteAsync(Guid contractorId)
        {
            throw new NotImplementedException();
        }
    }
}
