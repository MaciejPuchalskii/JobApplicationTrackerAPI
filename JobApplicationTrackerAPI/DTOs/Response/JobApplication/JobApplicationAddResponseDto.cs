namespace JobApplicationTrackerAPI.DTOs.Response.JobApplication
{
    public class JobApplicationAddResponseDto
    {
        public Guid Id { get; set; }
        public string Position { get; set; }
        public DateTime AppliedDate { get; set; }
    }
}