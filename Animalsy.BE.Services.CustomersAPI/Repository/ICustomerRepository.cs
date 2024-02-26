using Animalsy.BE.Services.CustomersAPI.Models;

namespace Animalsy.BE.Services.CustomersAPI.Repository
{
    public interface ICustomerRepository
    {
        Task<Guid> CreateAsync(Customer customer);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(Guid customerId);
        Task<bool> TryUpdateAsync(Guid customerId, Customer customer);
        Task<bool> TryDeleteAsync(Guid customerId);
    }
}
