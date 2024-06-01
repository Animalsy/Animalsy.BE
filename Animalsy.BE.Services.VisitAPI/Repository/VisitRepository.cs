using Animalsy.BE.Services.VisitAPI.Models.Dto;

namespace Animalsy.BE.Services.VisitAPI.Repository
{
    public class VisitRepository : IVisitRepository
    {
        public Task<VisitResponseDto> GetByIdAsync(Guid visitId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VisitResponseDto>> GetByVendorIdAsync(Guid vendorId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VisitResponseDto>> GetByCustomerIdAsync(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateAsync(CreateVisitDto visitDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryUpdateAsync(UpdateVisitDto visitDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryDeleteAsync(Guid visitId)
        {
            throw new NotImplementedException();
        }
    }
}
