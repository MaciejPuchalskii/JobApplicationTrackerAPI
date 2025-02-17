using JobApplicationTrackerAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationTrackerAPI.Repository.Company
{
    public class CompanyRepository : GenericRepository<Models.Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistByName(string name)
        {
            return await _context.Companies.AnyAsync(c => c.Name == name);
        }
    }
}