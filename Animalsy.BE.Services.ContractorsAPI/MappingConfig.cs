using Animalsy.BE.Services.ContractorsAPI.Models;
using Animalsy.BE.Services.ContractorsAPI.Models.Dto;
using AutoMapper;

namespace Animalsy.BE.Services.ContractorsAPI;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        return new MapperConfiguration(config =>
        {
            config.CreateMap<Contractor, ContractorResponseDto>();
            config.CreateMap<CreateContractorDto, Contractor>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        });
    }
}