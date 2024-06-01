using Animalsy.BE.Services.VisitAPI.Models.Dto;

namespace Animalsy.BE.Services.VisitAPI.Repository.Builder;

public interface IVisitResponseBuilder
{
    IVisitResponseBuilder WithCustomer();
    IVisitResponseBuilder WithContractor();
    IVisitResponseBuilder WithPet();
    IVisitResponseBuilder WithProduct();
    IVisitResponseBuilder WithVendor();
    Task<VisitResponseDto> BuildAsync();
}