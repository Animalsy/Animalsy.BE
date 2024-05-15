using Animalsy.BE.Services.PetsAPI.Data;
using Animalsy.BE.Services.PetsAPI.Models;
using Animalsy.BE.Services.PetsAPI.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.PetsAPI.Repository;

public class PetRepository(AppDbContext dbContext, IMapper mapper) : IPetRepository
{
    public async Task<Guid> CreateAsync(CreatePetDto petDto)
    {
        var pet = mapper.Map<Pet>(petDto);
        await dbContext.Pets.AddAsync(pet);
        await dbContext.SaveChangesAsync();
        return pet.Id;
    }

    public async Task<IEnumerable<PetResponseDto>> GetByCustomerAsync(Guid customerId)
    {
        var results = await dbContext.Pets
            .Where(pet => pet.CustomerId == customerId)
            .ToListAsync();
        return mapper.Map<IEnumerable<PetResponseDto>>(results);
    }

    public async Task<PetResponseDto?> GetByIdAsync(Guid petId)
    {
        var pet = await dbContext.Pets.FirstOrDefaultAsync(p => p.Id == petId);
        return mapper.Map<PetResponseDto>(pet);
    }

    public async Task<bool> TryUpdateAsync(UpdatePetDto petDto)
    {
        var existingPet = await dbContext.Pets.FirstOrDefaultAsync(p => p.Id == petDto.Id);
        if (existingPet == null) return false;

        existingPet.Name = petDto.Name;
        existingPet.Species = petDto.Species;
        existingPet.Race = petDto.Race;
        existingPet.DateOfBirth = petDto.DateOfBirth;
        existingPet.ImageUrl = petDto.ImageUrl;
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> TryDeleteAsync(Guid petId)
    {
        var existingPet = await dbContext.Pets.FirstOrDefaultAsync(p => p.Id == petId);
        if (existingPet == null) return false;

        dbContext.Pets.Remove(existingPet);
        await dbContext.SaveChangesAsync();
        return true;
    }
}