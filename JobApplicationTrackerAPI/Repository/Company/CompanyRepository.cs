using JobApplicationTrackerAPI.Data;

namespace JobApplicationTrackerAPI.Repository.Company
{
    public class CompanyRepository : GenericRepository<Models.Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}