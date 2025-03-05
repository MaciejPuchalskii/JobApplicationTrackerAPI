using JobApplicationTrackerAPI.Data.Enums;
using JobApplicationTrackerAPI.Models;

namespace JobApplicationTrackerAPI.DTOs.Response.JobApplication
{
    public class JobApplicationGetAllResponseDto
    {
        public Guid Id { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public JobApplicationStatus Status { get; set; }
        public DateTime AppliedDate { get; set; }
        public Guid CompanyId { get; set; }
    }
}