namespace JobApplicationTrackerAPI.Data.Models
{
    public class Company
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ContactPerson { get; set; }

        public string? PhoneNumber { get; set; }
        public string? JobAdvertURL { get; set; }
        public ICollection<JobApplication> JobApplications { get; set; } = [];
    }
}