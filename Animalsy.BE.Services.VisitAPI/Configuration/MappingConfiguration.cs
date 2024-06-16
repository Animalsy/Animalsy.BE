using Animalsy.BE.Services.VisitAPI.Models;
using Animalsy.BE.Services.VisitAPI.Models.Dto;
using Animalsy.BE.Services.VisitAPI.Models.Dto.Enums;
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
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => VisitStatus.Pending));
        });
    }
}