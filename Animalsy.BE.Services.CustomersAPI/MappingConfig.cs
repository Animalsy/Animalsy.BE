using Animalsy.BE.Services.CustomersAPI.Models;
using Animalsy.BE.Services.CustomersAPI.Models.Dto;
using AutoMapper;

namespace Animalsy.BE.Services.CustomersAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<Customer, CustomerResponseDto>();
                config.CreateMap<CreateCustomerDto, Customer>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            });
        }
    }
}
