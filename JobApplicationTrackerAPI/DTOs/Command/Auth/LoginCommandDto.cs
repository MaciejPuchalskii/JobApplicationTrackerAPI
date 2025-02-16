using System.ComponentModel.DataAnnotations;

namespace JobApplicationTrackerAPI.DTOs.Command.Auth
{
    public class LoginCommandDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}