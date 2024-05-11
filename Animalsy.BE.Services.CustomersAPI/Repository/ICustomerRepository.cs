using Animalsy.BE.Services.CustomersAPI.Models.Dto;

namespace Animalsy.BE.Services.CustomersAPI.Repository
{
    public interface ICustomerRepository
    {
        Task<Guid> CreateAsync(CreateCustomerDto customerDto);
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task<CustomerDto> GetByIdAsync(Guid customerId);
        Task<bool> TryUpdateAsync(Guid customerId, UpdateCustomerDto customerDto);
        Task<bool> TryDeleteAsync(Guid customerId);
    }
}
