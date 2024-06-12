using Animalsy.BE.Services.AuthAPI.Models;
using Animalsy.BE.Services.AuthAPI.Models.Dto;
using AutoMapper;

namespace Animalsy.BE.Services.AuthAPI.Configuration;

public class MappingConfiguration
{
    public static MapperConfiguration RegisterMaps()
    {
        return new MapperConfiguration(config =>
        {
            config.CreateMap<RegisterUserDto, ApplicationUser>()
                .ForMember(user => user.Email, opt => opt.MapFrom(dto => dto.EmailAddress))
                .ForMember(user => user.NormalizedEmail, opt => opt.MapFrom(dto => dto.EmailAddress))
                .ForMember(user => user.UserName, opt => opt.MapFrom(dto => dto.EmailAddress));

            config.CreateMap<RegisterUserDto, CreateCustomerDto>();
        });
    }
}