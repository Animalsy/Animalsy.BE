﻿using Animalsy.BE.Services.PetAPI.Data;
using Animalsy.BE.Services.PetAPI.Models;
using Animalsy.BE.Services.PetAPI.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.PetAPI.Repository;

public class PetRepository : IPetRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public PetRepository(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Guid> CreateAsync(CreatePetDto createPetDto)
    {
        var pet = _mapper.Map<Pet>(createPetDto);
        await _dbContext.Pets.AddAsync(pet);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        return pet.Id;
    }

    public async Task<IEnumerable<PetDto>> GetByCustomerAsync(Guid customerId)
    {
        var results = await _dbContext.Pets
            .Where(pet => pet.CustomerId == customerId)
            .ToListAsync();
        return _mapper.Map<IEnumerable<PetDto>>(results);
    }

    public async Task<PetDto> GetByIdAsync(Guid petId)
    {
        var pet = await _dbContext.Pets.FirstOrDefaultAsync(p => p.Id == petId);
        return _mapper.Map<PetDto>(pet);
    }

    public async Task<bool> TryUpdateAsync(UpdatePetDto updatePetDto)
    {
        var existingPet = await _dbContext.Pets.FirstOrDefaultAsync(p => p.Id == updatePetDto.Id);
        if (existingPet == null) return false;

        existingPet.Name = updatePetDto.Name;
        existingPet.Species = updatePetDto.Species;
        existingPet.Race = updatePetDto.Race;
        existingPet.DateOfBirth = updatePetDto.DateOfBirth;
        existingPet.ImageUrl = updatePetDto.ImageUrl;
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        return true;
    }

    public async Task DeleteAsync(PetDto petDto)
    {
        _dbContext.Pets.Remove(_mapper.Map<Pet>(petDto));
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
    }
}