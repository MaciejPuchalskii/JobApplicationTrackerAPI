using System.ComponentModel.DataAnnotations;

namespace JobApplicationTrackerAPI.DTOs.Command.Company
{
    public class AddCompanyCommandDto
    {
        [Required(ErrorMessage = "Company name is required")]
        [StringLength(100, ErrorMessage = "Company name cannot exceed 100 characters")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string? Description { get; set; }

        public string? ContactPerson { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [Url]
        public string? CompanyURL { get; set; }
    }
}