using JobApplicationTrackerAPI.Repository.Company;

namespace JobApplicationTrackerAPI.Service.CompanyService
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
    }
}