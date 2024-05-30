using Animalsy.BE.Services.CustomerAPI.Models.Dto;

namespace Animalsy.BE.Services.CustomerAPI.Services.Interfaces
{
    public interface IPetsService
    {
        Task<IEnumerable<PetDto>> GetPetsAsync(Guid customerId);
    }
}
