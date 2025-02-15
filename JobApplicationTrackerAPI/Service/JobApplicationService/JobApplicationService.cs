using JobApplicationTrackerAPI.Repository.JobApplication;

namespace JobApplicationTrackerAPI.Service.JobApplicationService
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;

        public JobApplicationService(IJobApplicationRepository jobApplicationRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
        }
    }
}