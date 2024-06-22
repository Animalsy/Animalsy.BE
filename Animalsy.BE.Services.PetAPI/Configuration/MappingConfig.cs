using Animalsy.BE.Services.PetAPI.Models;
using Animalsy.BE.Services.PetAPI.Models.Dto;
using AutoMapper;

namespace Animalsy.BE.Services.PetAPI.Configuration;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        return new MapperConfiguration(config =>
        {
            config.CreateMap<Pet, PetDto>();
            config.CreateMap<CreatePetDto, Pet>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        });

    }
}