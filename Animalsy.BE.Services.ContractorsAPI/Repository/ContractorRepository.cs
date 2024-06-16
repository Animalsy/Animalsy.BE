using Animalsy.BE.Services.ContractorAPI.Data;
using Animalsy.BE.Services.ContractorAPI.Models;
using Animalsy.BE.Services.ContractorAPI.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

    public async Task<IEnumerable<ContractorDto>> GetByVendorAsync(Guid vendorId)
    {
        var results = await _dbContext.Contractors
            .Where(p => p.VendorId == vendorId)
            .ToListAsync();
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

    public async Task DeleteAsync(ContractorDto contractorDto)
    {
        _dbContext.Contractors.Remove(_mapper.Map<Contractor>(contractorDto));
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
    }
}