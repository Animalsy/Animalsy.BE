using Animalsy.BE.Services.CustomersAPI.Models.Dto;

namespace Animalsy.BE.Services.CustomersAPI.Services.Interfaces
{
    public interface IPetsService
    {
        Task<IEnumerable<PetDto>> GetPetsAsync(Guid customerId);
    }
}
