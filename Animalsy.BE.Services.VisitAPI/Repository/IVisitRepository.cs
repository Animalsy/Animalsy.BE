using Animalsy.BE.Services.VisitAPI.Models.Dto;

namespace Animalsy.BE.Services.VisitAPI.Repository
{
    public interface IVisitRepository
    {
        Task<VisitDto> GetByIdAsync(Guid visitId);
        Task<IEnumerable<VisitDto>> GetByVendorIdAsync(Guid vendorId);
        Task<IEnumerable<VisitDto>> GetByCustomerIdAsync(Guid customerId);
        Task<Guid> CreateAsync(CreateVisitDto visitDto);
        Task<bool> TryUpdateAsync(UpdateVisitDto visitDto);
        Task<bool> TryDeleteAsync(Guid visitId);
    }
}
