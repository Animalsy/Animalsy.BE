﻿using Animalsy.BE.Services.PetAPI.Models;
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

        public async Task<IEnumerable<Pet>> GetAllAsync()
        {
            return await _dbContext.Pets.ToListAsync();
        }

        public async Task<IEnumerable<Pet>> GetByCustomerAsync(Guid customerId)
        {
            return await _dbContext.Pets
                .Where(pet => pet.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<Pet?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Pets.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> TryUpdateAsync(Guid id, Pet pet)
        {
            var existingPet = await _dbContext.Pets.FirstOrDefaultAsync(p => p.Id == id);
            if (existingPet == null) return false;

            existingPet.Name = pet.Name;
            existingPet.Species = pet.Species;
            existingPet.Race = pet.Race;
            existingPet.DateOfBirth = pet.DateOfBirth;
            return true;
        }

        public async Task<bool> TryDeleteAsync(Guid id)
        {
            var existingPet = await _dbContext.Pets.FirstOrDefaultAsync(p => p.Id == id);
            if (existingPet == null) return false;

            _dbContext.Pets.Remove(existingPet);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}