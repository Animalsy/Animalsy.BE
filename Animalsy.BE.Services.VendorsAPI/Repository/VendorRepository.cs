using Animalsy.BE.Services.VendorAPI.Data;
using Animalsy.BE.Services.VendorAPI.Models;
using Animalsy.BE.Services.VendorAPI.Models.Dto;
using Animalsy.BE.Services.VendorAPI.Repository.Builder;
using Animalsy.BE.Services.VendorAPI.Repository.ResponseHandler;
using Animalsy.BE.Services.VendorAPI.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.VendorAPI.Repository;

public class VendorRepository : IVendorRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IApiService _apiService;
    private readonly IResponseHandler _responseHandler;
    private readonly IMapper _mapper;

    public VendorRepository(AppDbContext dbContext, IApiService apiService, IResponseHandler responseHandler, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
        _responseHandler = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<VendorDto>> GetAllAsync()
    {
        var results = await _dbContext.Vendors.ToListAsync();
        return _mapper.Map<IEnumerable<VendorDto>>(results);
    }

    public async Task<IEnumerable<VendorDto>> GetByNameAsync(string name)
    {
        var results = await _dbContext.Vendors.ToListAsync();
        return _mapper.Map<IEnumerable<VendorDto>>(results);
    }

    public async Task<VendorDto> GetByIdAsync(Guid vendorId)
    {
        var result = await _dbContext.Vendors.FirstOrDefaultAsync(c => c.Id == vendorId);
        return _mapper.Map<VendorDto>(result);
    }

    public async Task<VendorDto> GetByEmailAsync(string email)
    {
        var result = await _dbContext.Vendors.FirstOrDefaultAsync(c => c.EmailAddress == email);
        return _mapper.Map<VendorDto>(result);
    }

    public async Task<VendorProfileDto> GetVendorProfileAsync(Guid vendorId)
    {
        var vendor = await _dbContext.Vendors.FirstOrDefaultAsync(c => c.Id == vendorId);

        return vendor != null
            ? await new VendorProfileBuilder(_apiService, _responseHandler, _mapper.Map<VendorDto>(vendor))
                .WithContractors()
                .WithVisits()
                .BuildAsync()
            : null;
    }

    public async Task<Guid> CreateAsync(CreateVendorDto vendorDto)
    {
        var vendor = _mapper.Map<Vendor>(vendorDto);
        await _dbContext.Vendors.AddAsync(vendor);
        await _dbContext.SaveChangesAsync();
        return vendor.Id;
    }

    public async Task<bool> TryUpdateAsync(UpdateVendorDto vendorDto)
    {
        var existingCustomer = await _dbContext.Vendors.FirstOrDefaultAsync(c => c.Id == vendorDto.Id);
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

        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> TryDeleteAsync(Guid vendorId)
    {
        var existingVendor = await _dbContext.Vendors.FirstOrDefaultAsync(c => c.Id == vendorId);
        if (existingVendor == null) return false;

        _dbContext.Vendors.Remove(existingVendor);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}