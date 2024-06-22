using Animalsy.BE.Services.CustomerAPI.Models;
using Animalsy.BE.Services.CustomerAPI.Models.Dto;
using AutoMapper;

namespace Animalsy.BE.Services.CustomerAPI.Configuration;

public class MappingConfiguration
{
    public static MapperConfiguration RegisterMaps()
    {
        return new MapperConfiguration(config =>
        {
            config.CreateMap<Customer, CustomerDto>();
            config.CreateMap<CreateCustomerDto, Customer>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        });
    }
}