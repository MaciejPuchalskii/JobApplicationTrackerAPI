using JobApplicationTrackerAPI.Repository.Attachment;

namespace JobApplicationTrackerAPI.Service.Attachment
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _attachmentRepository;

        public AttachmentService(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
        }
    }
}