namespace JobApplicationTrackerAPI.Service.CompanyService
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyService _companyService;

        public CompanyService(ICompanyService companyService)
        {
            _companyService = companyService;
        }
    }
}