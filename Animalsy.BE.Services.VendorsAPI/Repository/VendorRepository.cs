using Animalsy.BE.Services.VendorAPI.Data;
using Animalsy.BE.Services.VendorAPI.Models;
using Animalsy.BE.Services.VendorAPI.Models.Dto;
using Animalsy.BE.Services.VendorAPI.Repository.Builder.Factory;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Animalsy.BE.Services.VendorAPI.Repository;

public class VendorRepository : IVendorRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IVendorProfileBuilderFactory _vendorProfileBuilderFactory;
    private readonly IMapper _mapper;

    public VendorRepository(AppDbContext dbContext, IVendorProfileBuilderFactory vendorProfileBuilderFactory, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _vendorProfileBuilderFactory = vendorProfileBuilderFactory ?? throw new ArgumentNullException(nameof(vendorProfileBuilderFactory));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<VendorDto>> GetAllAsync()
    {
        var results = await _dbContext.Vendors.ToListAsync();
        return _mapper.Map<IEnumerable<VendorDto>>(results);
    }

    public async Task<VendorDto> GetByIdAsync(Guid vendorId)
    {
        var result = await _dbContext.Vendors.FirstOrDefaultAsync(c => c.Id == vendorId);
        return _mapper.Map<VendorDto>(result);
    }

    public async Task<IEnumerable<VendorProfileDto>> GetVendorProfilesAsync(Guid userId)
    {
        var vendors = await _dbContext.Vendors.Where(c => c.UserId == userId).ToListAsync();

        if (vendors.IsNullOrEmpty()) return [];

        var results = vendors.Select(vendor =>
            _vendorProfileBuilderFactory.Create(_mapper.Map<VendorDto>(vendor))
                .WithContractors()
                .WithVisits()
                .BuildAsync());

        return await Task.WhenAll(results).ConfigureAwait(false);
    }

    public async Task<Guid> CreateAsync(CreateVendorDto createVendorDto)
    {
        var vendor = _mapper.Map<Vendor>(createVendorDto);
        await _dbContext.Vendors.AddAsync(vendor);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        return vendor.Id;
    }

    public async Task<bool> TryUpdateAsync(UpdateVendorDto updateVendorDto)
    {
        var existingCustomer = await _dbContext.Vendors.FirstOrDefaultAsync(c => c.Id == updateVendorDto.Id);
        if (existingCustomer == null) return false;

        existingCustomer.Name = updateVendorDto.Name;
        existingCustomer.Nip = updateVendorDto.Nip;
        existingCustomer.City = updateVendorDto.City;
        existingCustomer.Street = updateVendorDto.Street;
        existingCustomer.Building = updateVendorDto.Building;
        existingCustomer.Flat = updateVendorDto.Flat;
        existingCustomer.PostalCode = updateVendorDto.PostalCode;
        existingCustomer.PhoneNumber = updateVendorDto.PhoneNumber;
        existingCustomer.EmailAddress = updateVendorDto.EmailAddress;

        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        return true;
    }

    public async Task DeleteAsync(VendorDto vendorDto)
    {
        _dbContext.Vendors.Remove(_mapper.Map<Vendor>(vendorDto));
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
    }
}