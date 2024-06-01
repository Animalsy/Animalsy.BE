using Animalsy.BE.Services.VisitAPI.Models.Dto;

namespace Animalsy.BE.Services.VisitAPI.Repository
{
    public interface IVisitRepository
    {
        Task<VisitResponseDto> GetByIdAsync(Guid visitId);
        Task<IEnumerable<VisitResponseDto>> GetByVendorIdAsync(Guid vendorId);
        Task<IEnumerable<VisitResponseDto>> GetByCustomerIdAsync(Guid customerId);
        Task<Guid> CreateAsync(CreateVisitDto visitDto);
        Task<bool> TryUpdateAsync(UpdateVisitDto visitDto);
        Task<bool> TryDeleteAsync(Guid visitId);
    }
}
