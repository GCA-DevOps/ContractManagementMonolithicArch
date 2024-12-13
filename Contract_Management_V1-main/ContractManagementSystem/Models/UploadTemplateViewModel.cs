using System.ComponentModel.DataAnnotations;

namespace ContractManagementSystem.Models
{
    public class UploadTemplateViewModel
    {
        [Required(ErrorMessage = "The Template Name field is required.")]
        public string TemplateName { get; set; }

        [Required(ErrorMessage = "The Template Description field is required.")]
        public string TemplateDescription { get; set; }

        [Required(ErrorMessage = "The Template File field is required.")]
        public IFormFile TemplateFile { get; set; }

        // Removed the [Required] attribute; this should not be a required field for form submission
        public List<Template> Templates { get; set; }
    }

    public class Template
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string Creator { get; set; }
        public string? FilePath { get; set; }
    }
}
