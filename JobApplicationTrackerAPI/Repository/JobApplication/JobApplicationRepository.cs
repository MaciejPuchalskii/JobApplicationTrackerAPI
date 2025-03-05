using JobApplicationTrackerAPI.Data;

namespace JobApplicationTrackerAPI.Repository.JobApplication
{
    public class JobApplicationRepository : GenericRepository<Models.JobApplication>, IJobApplicationRepository
    {
        private readonly ApplicationDbContext _context;

        public JobApplicationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Models.JobApplication>> GetByCompanyId(Guid companyId)
        {
            return _context.JobApplications.Where(application => application.CompanyId == companyId).ToList();
        }
    }
}