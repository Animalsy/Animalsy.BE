using Animalsy.BE.Services.CustomersAPI.Models.Dto;
using FluentValidation;

namespace Animalsy.BE.Services.CustomersAPI.Validators
{
    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerDto>
    {
        public UpdateCustomerValidator(UniqueIdValidator uniqueIdValidator, EmailValidator emailValidator, PhoneNumberValidator phoneNumberValidator)
        {
            RuleFor(x => x.Id).SetValidator(uniqueIdValidator);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(20);
            RuleFor(x => x.City).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Street).NotEmpty().MaximumLength(40);
            RuleFor(x => x.Building).NotEmpty().MaximumLength(20);
            RuleFor(x => x.PostalCode).NotEmpty().Length(6);
            RuleFor(x => x.Flat).Length(1, 20).When(x => x.Flat is not null);
            RuleFor(x => x.EmailAddress).SetValidator(emailValidator);
            RuleFor(x => x.PhoneNumber).SetValidator(phoneNumberValidator);
        }
    }
}
