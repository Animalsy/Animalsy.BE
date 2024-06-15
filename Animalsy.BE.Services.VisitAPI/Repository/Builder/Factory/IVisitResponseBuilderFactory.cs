using Animalsy.BE.Services.VisitAPI.Models.Dto;

namespace Animalsy.BE.Services.VisitAPI.Repository.Builder.Factory;

public interface IVisitResponseBuilderFactory
{
    IVisitResponseBuilder Create(VisitDto visit);
}