using Animalsy.BE.Services.VisitAPI.Models;
using Animalsy.BE.Services.VisitAPI.Models.Dto;
using AutoMapper;

namespace Animalsy.BE.Services.VisitAPI.Configuration;

public class MappingConfiguration
{
    public static MapperConfiguration RegisterMaps()
    {
        return new MapperConfiguration(config =>
        {
            config.CreateMap<Visit, VisitDto>();
            config.CreateMap<CreateVisitDto, Visit>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        });
    }
}