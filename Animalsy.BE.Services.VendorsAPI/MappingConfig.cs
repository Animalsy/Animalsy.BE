using Animalsy.BE.Services.VendorsAPI.Models;
using Animalsy.BE.Services.VendorsAPI.Models.Dto;
using AutoMapper;

namespace Animalsy.BE.Services.VendorsAPI
{
    public class MappingConfig
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
}
