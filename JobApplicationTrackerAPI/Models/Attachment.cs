namespace JobApplicationTrackerAPI.Models
{
    public class Attachment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string FileType { get; set; }

        public long FileSize { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public Guid JobApplicationId { get; set; }
    }
}