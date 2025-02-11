namespace JobApplicationTrackerAPI.Models
{
    public class Note
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid JobApplicationId { get; set; }
    }
}