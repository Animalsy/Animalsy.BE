﻿using Animalsy.BE.Services.PetsAPI.Models.Dto;

namespace Animalsy.BE.Services.PetsAPI.Repository
{
    public interface IPetRepository
    {
        Task<Guid> CreateAsync(CreatePetDto petDto);
        Task<IEnumerable<PetDto>> GetByCustomerAsync(Guid customerId);
        Task<PetDto?> GetByIdAsync(Guid petId);
        Task<bool> TryUpdateAsync(UpdatePetDto petDto);
        Task<bool> TryDeleteAsync(Guid petId);
    }
}
