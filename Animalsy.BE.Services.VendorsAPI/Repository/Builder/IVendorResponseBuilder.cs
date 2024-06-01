using Animalsy.BE.Services.VendorAPI.Models.Dto;

namespace Animalsy.BE.Services.VendorAPI.Repository.Builder;

public interface IVendorResponseBuilder
{
    IVendorResponseBuilder WithContractors();
    IVendorResponseBuilder WithVisits();
    Task<VendorResponseDto> BuildAsync();
}