using JobApplicationTrackerAPI.Data;

namespace JobApplicationTrackerAPI.Repository.AttachmentRepository
{
    public class AttachmentRepository : GenericRepository<Models.Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}