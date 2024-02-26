using Animalsy.BE.Services.PetAPI.Models;
using Animalsy.BE.Services.PetsAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.PetsAPI.Repository
{
    public class PetRepository : IPetRepository
    {
        private readonly AppDbContext _dbContext;

        public PetRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> CreateAsync(Pet pet)
        {
            await _dbContext.Pets.AddAsync(pet);
            await _dbContext.SaveChangesAsync();
            return pet.Id;
        }

        public async Task<IEnumerable<Pet>> GetByCustomerAsync(Guid customerId)
        {
            return await _dbContext.Pets
                .Where(pet => pet.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<Pet?> GetByIdAsync(Guid petId)
        {
            return await _dbContext.Pets.FirstOrDefaultAsync(p => p.Id == petId);
        }

        public async Task<bool> TryUpdateAsync(Guid petId, Pet pet)
        {
            var existingPet = await _dbContext.Pets.FirstOrDefaultAsync(p => p.Id == petId);
            if (existingPet == null) return false;

            existingPet.Name = pet.Name;
            existingPet.Species = pet.Species;
            existingPet.Race = pet.Race;
            existingPet.DateOfBirth = pet.DateOfBirth;
            existingPet.ImageUrl = pet.ImageUrl;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TryDeleteAsync(Guid petId)
        {
            var existingPet = await _dbContext.Pets.FirstOrDefaultAsync(p => p.Id == petId);
            if (existingPet == null) return false;

            _dbContext.Pets.Remove(existingPet);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
