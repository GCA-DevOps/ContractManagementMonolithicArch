using CMS.Application.DTOs.Request;
using CMS.Application.Interfaces.NegotiationInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace CMS.Application.Validators
{
    public class ValidateNegotiatorEmail : ValidationAttribute
    {


        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var _negotiationService = validationContext.GetRequiredService<INegotiationService>();

            var negotiator = validationContext.ObjectInstance as NegotiationsDto;
            if (negotiator == null || string.IsNullOrWhiteSpace(negotiator.Email))
            {
                return new ValidationResult("Invalid negotiator details");
            }

            var emailExists = _negotiationService.EmailExist(negotiator.Email);
            if (emailExists)
            {
                return new ValidationResult("Email already exists");
            }

            return ValidationResult.Success;
        }
    }
}





