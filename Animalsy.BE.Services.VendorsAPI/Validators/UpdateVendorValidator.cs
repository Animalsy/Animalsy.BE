using Animalsy.BE.Services.VendorsAPI.Models.Dto;
using FluentValidation;

namespace Animalsy.BE.Services.VendorsAPI.Validators
{
    public class UpdateVendorValidator : AbstractValidator<UpdateVendorDto>
    {
        public UpdateVendorValidator(UniqueIdValidator uniqueIdValidator, EmailValidator emailValidator)
        {
            RuleFor(x => x.Id).SetValidator(uniqueIdValidator);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Nip).SetValidator(new NumberValidator("Nip", 10));
            RuleFor(x => x.City).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Street).NotEmpty().MaximumLength(40);
            RuleFor(x => x.Building).NotEmpty().MaximumLength(5);
            RuleFor(x => x.PostalCode).NotEmpty().Length(6);
            RuleFor(x => x.Flat).MaximumLength(5).When(x => x.Flat is not null);
            RuleFor(x => x.EmailAddress).SetValidator(emailValidator);
            RuleFor(x => x.PhoneNumber).SetValidator(new NumberValidator("Phone", 9));
        }
    }
}
