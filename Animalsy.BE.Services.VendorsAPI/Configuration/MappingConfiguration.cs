using Animalsy.BE.Services.VendorAPI.Models;
using Animalsy.BE.Services.VendorAPI.Models.Dto;
using AutoMapper;

namespace Animalsy.BE.Services.VendorAPI.Configuration;

public class MappingConfiguration
{
    public static MapperConfiguration RegisterMaps()
    {
        return new MapperConfiguration(config =>
        {
            config.CreateMap<Vendor, VendorResponseDto>();
            config.CreateMap<CreateVendorDto, Vendor>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        });
    }
}