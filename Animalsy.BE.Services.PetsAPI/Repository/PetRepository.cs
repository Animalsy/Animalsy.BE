using Animalsy.BE.Services.PetAPI.Data;
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

    public async Task<Guid> CreateAsync(CreatePetDto petDto)
    {
        var pet = _mapper.Map<Pet>(petDto);
        await _dbContext.Pets.AddAsync(pet);
        await _dbContext.SaveChangesAsync();
        return pet.Id;
    }

    public async Task<IEnumerable<PetResponseDto>> GetByCustomerAsync(Guid customerId)
    {
        var results = await _dbContext.Pets
            .Where(pet => pet.CustomerId == customerId)
            .ToListAsync();
        return _mapper.Map<IEnumerable<PetResponseDto>>(results);
    }

    public async Task<PetResponseDto> GetByIdAsync(Guid petId)
    {
        var pet = await _dbContext.Pets.FirstOrDefaultAsync(p => p.Id == petId);
        return _mapper.Map<PetResponseDto>(pet);
    }

    public async Task<bool> TryUpdateAsync(UpdatePetDto petDto)
    {
        var existingPet = await _dbContext.Pets.FirstOrDefaultAsync(p => p.Id == petDto.Id);
        if (existingPet == null) return false;

        existingPet.Name = petDto.Name;
        existingPet.Species = petDto.Species;
        existingPet.Race = petDto.Race;
        existingPet.DateOfBirth = petDto.DateOfBirth;
        existingPet.ImageUrl = petDto.ImageUrl;
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