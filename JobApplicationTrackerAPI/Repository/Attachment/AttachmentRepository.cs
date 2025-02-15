using JobApplicationTrackerAPI.Data;

namespace JobApplicationTrackerAPI.Repository.Attachment
{
    public class AttachmentRepository : GenericRepository<Models.Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}