using CMS.Application.DTOs.Request;
using CMS.Application.Interfaces.CollaborationInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace CMS.Application.Validators
{
    public class ValidateCollaboratorEmail : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var _collaborationService = validationContext.GetRequiredService<ICollaborationService>();

            var collaborator = validationContext.ObjectInstance as CollaborationDto;
            if (collaborator == null || string.IsNullOrWhiteSpace(collaborator.Email))
            {
                return new ValidationResult("Invalid collaborator details");
            }

            var emailExists = _collaborationService.EmailExists(collaborator.Email);
            if (emailExists)
            {
                return new ValidationResult("Email already exists");
            }

            return ValidationResult.Success;
        }
    }
}
