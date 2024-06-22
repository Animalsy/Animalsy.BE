using Animalsy.BE.Services.VendorAPI.Models.Dto;

namespace Animalsy.BE.Services.VendorAPI.Repository.Builder.Factory;

public interface IVendorProfileBuilderFactory
{
    IVendorProfileBuilder Create(VendorDto vendor);
}