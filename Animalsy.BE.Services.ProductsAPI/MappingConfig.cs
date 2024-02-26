using Animalsy.BE.Services.ProductsAPI.Models;
using Animalsy.BE.Services.ProductsAPI.Models.Dto;
using AutoMapper;

namespace Animalsy.BE.Services.ProductsAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<Product, ProductDto>();
                config.CreateMap<UpdateProductDto, Product>();
                config.CreateMap<CreateProductDto, Product>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            });

        }
    }
}
