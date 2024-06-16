using Animalsy.BE.Services.ProductAPI.Models;
using Animalsy.BE.Services.ProductAPI.Models.Dto;
using AutoMapper;

namespace Animalsy.BE.Services.ProductAPI.Configuration;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        return new MapperConfiguration(config =>
        {
            config.CreateMap<Product, ProductDto>();
            config.CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        });

    }
}