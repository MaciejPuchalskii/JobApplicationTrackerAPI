using System.ComponentModel.DataAnnotations;

namespace JobApplicationTrackerAPI.DTOs.Command.Company
{
    public class UpdateCompanyCommandDto
    {
        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string? Description { get; set; }

        public string? ContactPerson { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [Url]
        public string? CompanyURL { get; set; }
    }
}