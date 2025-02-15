namespace JobApplicationTrackerAPI.Service.JobApplicationService
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationService _jobApplicationService;

        public JobApplicationService(IJobApplicationService jobApplicationService)
        {
            _jobApplicationService = jobApplicationService;
        }
    }
}