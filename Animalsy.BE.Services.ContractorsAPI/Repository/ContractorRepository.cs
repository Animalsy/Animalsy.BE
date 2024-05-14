using Animalsy.BE.Services.ContractorsAPI.Data;
using Animalsy.BE.Services.ContractorsAPI.Models;
using Animalsy.BE.Services.ContractorsAPI.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.ContractorsAPI.Repository
{
    public class ContractorRepository(AppDbContext dbContext, IMapper mapper) : IContractorRepository
    {
        public async Task<ContractorResponseDto?> GetByIdAsync(Guid contractorId)
        {
            var result = await dbContext.Contractors.FirstOrDefaultAsync(p => p.Id == contractorId);
            return mapper.Map<ContractorResponseDto>(result);
        }

        public async Task<IEnumerable<ContractorResponseDto>> GetByVendorAsync(Guid vendorId)
        {
            var results = await dbContext.Contractors
                .Where(p => p.VendorId == vendorId)
                .ToListAsync();
            return mapper.Map<IEnumerable<ContractorResponseDto>>(results);
        }

        public async Task<Guid> CreateAsync(CreateContractorDto contractorDto)
        {
            var contractor = mapper.Map<Contractor>(contractorDto);
            await dbContext.Contractors.AddAsync(contractor);
            await dbContext.SaveChangesAsync();
            return contractor.Id;
        }

        public async Task<bool> TryUpdateAsync(UpdateContractorDto contractorDto)
        {
            var existingContractor = await dbContext.Contractors.FirstOrDefaultAsync(p => p.Id == contractorDto.Id);
            if (existingContractor == null) return false;

            existingContractor.Name = contractorDto.Name;
            existingContractor.LastName = contractorDto.LastName;
            existingContractor.Specialization = contractorDto.Specialization;
            existingContractor.ImageUrl = contractorDto.ImageUrl;

            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TryDeleteAsync(Guid contractorId)
        {
            var existingContractor = await dbContext.Contractors.FirstOrDefaultAsync(p => p.Id == contractorId);
            if (existingContractor == null) return false;

            dbContext.Contractors.Remove(existingContractor);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
