using Animalsy.BE.Services.PetAPI.Models.Dto;

namespace Animalsy.BE.Services.PetAPI.Repository;

public interface IPetRepository
{
    Task<Guid> CreateAsync(CreatePetDto createPetDto);
    Task<IEnumerable<PetDto>> GetByCustomerAsync(Guid customerId);
    Task<PetDto> GetByIdAsync(Guid petId);
    Task<bool> TryUpdateAsync(UpdatePetDto updatePetDto);
    Task DeleteAsync(PetDto petDto);
}