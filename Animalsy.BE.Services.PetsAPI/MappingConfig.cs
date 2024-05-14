using Animalsy.BE.Services.PetsAPI.Models;
using Animalsy.BE.Services.PetsAPI.Models.Dto;
using AutoMapper;

namespace Animalsy.BE.Services.PetsAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<Pet, PetResponseDto>();
                config.CreateMap<CreatePetDto, Pet>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            });

        }
    }
}
