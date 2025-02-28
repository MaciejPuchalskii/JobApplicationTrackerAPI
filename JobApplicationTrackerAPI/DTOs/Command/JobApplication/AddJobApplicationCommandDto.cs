using JobApplicationTrackerAPI.Data.Enums;
using JobApplicationTrackerAPI.Models;

namespace JobApplicationTrackerAPI.DTOs.Command.JobApplication
{
    public class AddJobApplicationCommandDto
    {
        public string Position { get; set; }
        public string Description { get; set; }
        public JobApplicationStatus Status { get; set; }
        public DateTime AppliedDate { get; set; }
        public string? JobAdvertURL { get; set; }
        public Guid CompanyId { get; set; }
    }
}