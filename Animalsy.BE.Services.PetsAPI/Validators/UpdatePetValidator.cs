using Animalsy.BE.Services.PetsAPI.Models.Dto;
using FluentValidation;

namespace Animalsy.BE.Services.PetsAPI.Validators
{
    public class UpdatePetValidator : AbstractValidator<UpdatePetDto>
    {
        public UpdatePetValidator(UniqueIdValidator idValidator)
        {
            
        }
    }
}
