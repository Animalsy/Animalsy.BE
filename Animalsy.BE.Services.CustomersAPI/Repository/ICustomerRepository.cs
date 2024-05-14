using Animalsy.BE.Services.CustomersAPI.Models.Dto;

namespace Animalsy.BE.Services.CustomersAPI.Repository
{
    public interface ICustomerRepository
    {
        Task<Guid> CreateAsync(CreateCustomerDto customerDto);
        Task<IEnumerable<CustomerResponseDto>> GetAllAsync();
        Task<CustomerResponseDto?> GetByIdAsync(Guid customerId);
        Task<CustomerResponseDto?> GetByEmailAsync (string email);
        Task<bool> TryUpdateAsync(UpdateCustomerDto customerDto);
        Task<bool> TryDeleteAsync(Guid customerId);
    }
}
