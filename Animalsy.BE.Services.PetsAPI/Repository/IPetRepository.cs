using Animalsy.BE.Services.PetAPI.Models.Dto;

namespace Animalsy.BE.Services.PetAPI.Repository;

public interface IPetRepository
{
    Task<Guid> CreateAsync(CreatePetDto petDto);
    Task<IEnumerable<PetResponseDto>> GetByCustomerAsync(Guid customerId);
    Task<PetResponseDto> GetByIdAsync(Guid petId);
    Task<bool> TryUpdateAsync(UpdatePetDto petDto);
    Task<bool> TryDeleteAsync(Guid petId);
}