using Animalsy.BE.Services.VendorAPI.Models.Dto;

namespace Animalsy.BE.Services.VendorAPI.Repository.Builder;

public interface IVendorProfileBuilder
{
    IVendorProfileBuilder WithContractors();
    IVendorProfileBuilder WithVisits();
    Task<VendorProfileDto> BuildAsync();
}