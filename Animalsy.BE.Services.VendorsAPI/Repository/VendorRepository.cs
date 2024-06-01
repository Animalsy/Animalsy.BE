using Animalsy.BE.Services.VendorAPI.Data;
using Animalsy.BE.Services.VendorAPI.Models;
using Animalsy.BE.Services.VendorAPI.Models.Dto;
using Animalsy.BE.Services.VendorAPI.Repository.Builder;
using Animalsy.BE.Services.VendorAPI.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.VendorAPI.Repository;

public class VendorRepository(AppDbContext dbContext, IApiService apiService, IMapper mapper) : IVendorRepository
{
    public async Task<IEnumerable<VendorDto>> GetAllAsync()
    {
        var results = await dbContext.Vendors.ToListAsync();
        return mapper.Map<IEnumerable<VendorDto>>(results);
    }

    public async Task<IEnumerable<VendorDto>> GetByNameAsync(string name)
    {
        var results = await dbContext.Vendors.ToListAsync();
        return mapper.Map<IEnumerable<VendorDto>>(results);
    }

    public async Task<VendorDto> GetByIdAsync(Guid vendorId)
    {
        var result = await dbContext.Vendors.FirstOrDefaultAsync(c => c.Id == vendorId);
        return mapper.Map<VendorDto>(result);
    }

    public async Task<VendorDto> GetByEmailAsync(string email)
    {
        var result = await dbContext.Vendors.FirstOrDefaultAsync(c => c.EmailAddress == email);
        return mapper.Map<VendorDto>(result);
    }

    public async Task<VendorResponseDto> GetVendorProfileAsync(Guid vendorId)
    {
        var vendor = await dbContext.Vendors.FirstOrDefaultAsync(c => c.Id == vendorId);

        return vendor != null
            ? await new VendorResponseBuilder(apiService, mapper.Map<VendorDto>(vendor))
                .WithContractors()
                .WithVisits()
                .BuildAsync()
            : null;
    }

    public async Task<Guid> CreateAsync(CreateVendorDto vendorDto)
    {
        var vendor = mapper.Map<Vendor>(vendorDto);
        await dbContext.Vendors.AddAsync(vendor);
        await dbContext.SaveChangesAsync();
        return vendor.Id;
    }

    public async Task<bool> TryUpdateAsync(UpdateVendorDto vendorDto)
    {
        var existingCustomer = await dbContext.Vendors.FirstOrDefaultAsync(c => c.Id == vendorDto.Id);
        if (existingCustomer == null) return false;

        existingCustomer.Name =  vendorDto.Name;
        existingCustomer.Nip = vendorDto.Nip;
        existingCustomer.City = vendorDto.City;
        existingCustomer.Street = vendorDto.Street;
        existingCustomer.Building = vendorDto.Building;
        existingCustomer.Flat = vendorDto.Flat;
        existingCustomer.PostalCode = vendorDto.PostalCode;
        existingCustomer.PhoneNumber = vendorDto.PhoneNumber;
        existingCustomer.EmailAddress = vendorDto.EmailAddress;

        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> TryDeleteAsync(Guid vendorId)
    {
        var existingVendor = await dbContext.Vendors.FirstOrDefaultAsync(c => c.Id == vendorId);
        if (existingVendor == null) return false;

        dbContext.Vendors.Remove(existingVendor);
        await dbContext.SaveChangesAsync();
        return true;
    }
}