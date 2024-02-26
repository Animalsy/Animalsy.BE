using Animalsy.BE.Services.CustomersAPI.Models;

namespace Animalsy.BE.Services.CustomersAPI.Repository
{
    public interface ICustomerRepository
    {
        Task<Guid> CreateAsync(Customer customer);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(Guid id);
        Task<bool> TryUpdateAsync(Guid id, Customer customer);
        Task<bool> TryDeleteAsync(Guid id);
    }
}
