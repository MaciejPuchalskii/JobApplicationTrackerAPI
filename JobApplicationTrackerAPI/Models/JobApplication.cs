using JobApplicationTrackerAPI.Data.Enums;

namespace JobApplicationTrackerAPI.Models
{
    public class JobApplication
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Position { get; set; }
        public string Description { get; set; }
        public JobApplicationStatus Status { get; set; }
        public DateTime AppliedDate { get; set; }

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<Note> NotesList { get; set; } = [];
        public ICollection<Attachment> Attachments { get; set; } = [];
    }
}