namespace JobApplicationTrackerAPI.Service.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentService _attachmentService;

        public AttachmentService(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }
    }
}