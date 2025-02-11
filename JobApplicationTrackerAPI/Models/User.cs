using Microsoft.AspNetCore.Identity;

namespace JobApplicationTrackerAPI.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? LinkedInProfile { get; set; }
        public string? GitHubProfile { get; set; }
        public ICollection<JobApplication> JobApplications { get; set; } = [];
    }
}