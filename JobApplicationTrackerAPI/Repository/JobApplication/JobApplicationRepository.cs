using JobApplicationTrackerAPI.Data;

namespace JobApplicationTrackerAPI.Repository.JobApplication
{
    public class JobApplicationRepository : GenericRepository<Models.JobApplication>, IJobApplicationRepository
    {
        public JobApplicationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}