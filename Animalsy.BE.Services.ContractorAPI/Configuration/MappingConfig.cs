using Animalsy.BE.Services.ContractorAPI.Models;
using Animalsy.BE.Services.ContractorAPI.Models.Dto;
using AutoMapper;

namespace Animalsy.BE.Services.ContractorAPI.Configuration;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        return new MapperConfiguration(config =>
        {
            config.CreateMap<Contractor, ContractorDto>();
            config.CreateMap<CreateContractorDto, Contractor>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        });
    }
}