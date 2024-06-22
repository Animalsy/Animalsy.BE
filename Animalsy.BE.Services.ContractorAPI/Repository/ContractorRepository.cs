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

    public async Task<ContractorDto> GetByIdAsync(Guid contractorId)
    {
        var result = await _dbContext.Contractors.FirstOrDefaultAsync(p => p.Id == contractorId);
        return result != null ? _mapper.Map<ContractorDto>(result) : null;
    }

    public async Task<IEnumerable<ContractorDto>> GetByVendorAsync(Guid vendorId, string specialization = null)
    {
        var query = _dbContext.Contractors.Where(p => p.VendorId == vendorId);

        if (specialization != null)
        {
            query = query.Where(p => p.Specialization.StartsWith(specialization));
        }

        var results = await query.ToListAsync();
        return _mapper.Map<IEnumerable<ContractorDto>>(results);
    }

    public async Task<Guid> CreateAsync(CreateContractorDto createContractorDto)
    {
        var contractor = _mapper.Map<Contractor>(createContractorDto);
        await _dbContext.Contractors.AddAsync(contractor);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        return contractor.Id;
    }

    public async Task<bool> TryUpdateAsync(UpdateContractorDto updateContractorDto)
    {
        var existingContractor = await _dbContext.Contractors.FirstOrDefaultAsync(p => p.Id == updateContractorDto.Id);
        if (existingContractor == null) return false;

        existingContractor.Name = updateContractorDto.Name;
        existingContractor.LastName = updateContractorDto.LastName;
        existingContractor.Specialization = updateContractorDto.Specialization;
        existingContractor.ImageUrl = updateContractorDto.ImageUrl;

        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        return true;
    }

    public async Task<bool> TryDeleteAsync(Guid contractorId)
    {
        var existingContractor = await _dbContext.Contractors.FirstOrDefaultAsync(p => p.Id == contractorId);
        if (existingContractor == null) return false;

        _dbContext.Contractors.Remove(existingContractor);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        return true;
    }
}