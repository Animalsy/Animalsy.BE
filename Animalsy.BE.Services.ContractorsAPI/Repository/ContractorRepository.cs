using Animalsy.BE.Services.ContractorAPI.Data;
using Animalsy.BE.Services.ContractorAPI.Models;
using Animalsy.BE.Services.ContractorAPI.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.ContractorAPI.Repository;

public class ContractorRepository : IContractorRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public ContractorRepository(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ContractorResponseDto> GetByIdAsync(Guid contractorId)
    {
        try
        {
            var result = await _dbContext.Contractors.FirstOrDefaultAsync(p => p.Id == contractorId);
            return _mapper.Map<ContractorResponseDto>(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public async Task<IEnumerable<ContractorResponseDto>> GetByVendorAsync(Guid vendorId)
    {
        var results = await _dbContext.Contractors
            .Where(p => p.VendorId == vendorId)
            .ToListAsync();
        return _mapper.Map<IEnumerable<ContractorResponseDto>>(results);
    }

    public async Task<Guid> CreateAsync(CreateContractorDto contractorDto)
    {
        var contractor = _mapper.Map<Contractor>(contractorDto);
        await _dbContext.Contractors.AddAsync(contractor);
        await _dbContext.SaveChangesAsync();
        return contractor.Id;
    }

    public async Task<bool> TryUpdateAsync(UpdateContractorDto contractorDto)
    {
        var existingContractor = await _dbContext.Contractors.FirstOrDefaultAsync(p => p.Id == contractorDto.Id);
        if (existingContractor == null) return false;

        existingContractor.Name = contractorDto.Name;
        existingContractor.LastName = contractorDto.LastName;
        existingContractor.Specialization = contractorDto.Specialization;
        existingContractor.ImageUrl = contractorDto.ImageUrl;

        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> TryDeleteAsync(Guid contractorId)
    {
        var existingContractor = await _dbContext.Contractors.FirstOrDefaultAsync(p => p.Id == contractorId);
        if (existingContractor == null) return false;

        _dbContext.Contractors.Remove(existingContractor);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}