using Animalsy.BE.Services.PetAPI.Models;
using Animalsy.BE.Services.PetsAPI.Models.Dto;
using AutoMapper;

namespace Animalsy.BE.Services.PetsAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(config =>
                config.CreateMap<PetDto, Pet>().ReverseMap());
        }
    }
}
