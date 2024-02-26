using Animalsy.BE.Services.PetAPI.Models;

namespace Animalsy.BE.Services.PetsAPI.Repository
{
    public interface IPetRepository
    {
        Task<Guid> CreateAsync(Pet pet);
        Task<IEnumerable<Pet>> GetByCustomerAsync(Guid customerId);
        Task<Pet?> GetByIdAsync(Guid id);
        Task<bool> TryUpdateAsync(Guid id, Pet pet);
        Task<bool> TryDeleteAsync(Guid id);
    }
}
