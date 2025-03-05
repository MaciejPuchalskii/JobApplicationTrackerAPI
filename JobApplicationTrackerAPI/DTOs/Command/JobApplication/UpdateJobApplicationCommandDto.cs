using JobApplicationTrackerAPI.Data.Enums;

namespace JobApplicationTrackerAPI.DTOs.Command.JobApplication
{
    public class UpdateJobApplicationCommandDto
    {
        public Guid Id { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public JobApplicationStatus Status { get; set; }
        public DateTime AppliedDate { get; set; }
        public string? JobAdvertURL { get; set; }
        public Guid CompanyId { get; set; }
    }
}