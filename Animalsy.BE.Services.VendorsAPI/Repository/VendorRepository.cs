using Animalsy.BE.Services.VendorsAPI.Data;
using Animalsy.BE.Services.VendorsAPI.Models;
using Animalsy.BE.Services.VendorsAPI.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.VendorsAPI.Repository
{
    public class VendorRepository(AppDbContext dbContext, IMapper mapper) : IVendorRepository
    {
        public async Task<IEnumerable<VendorResponseDto>> GetAllAsync()
        {
            var results = await dbContext.Vendors.ToListAsync();
            return mapper.Map<IEnumerable<VendorResponseDto>>(results);
        }

        public async Task<IEnumerable<VendorResponseDto>> GetByNameAsync(string name)
        {
            var results = await dbContext.Vendors.ToListAsync();
            return mapper.Map<IEnumerable<VendorResponseDto>>(results);
        }

        public async Task<VendorResponseDto?> GetByIdAsync(Guid customerId)
        {
            var result = await dbContext.Vendors.FirstOrDefaultAsync(c => c.Id == customerId);
            return mapper.Map<VendorResponseDto>(result);
        }

        public async Task<VendorResponseDto?> GetByEmailAsync(string email)
        {
            var result = await dbContext.Vendors.FirstOrDefaultAsync(c => c.EmailAddress == email);
            return mapper.Map<VendorResponseDto>(result);
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
}
