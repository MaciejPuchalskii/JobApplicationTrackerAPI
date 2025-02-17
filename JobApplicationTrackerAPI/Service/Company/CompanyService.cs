using JobApplicationTrackerAPI.DTOs.Command.Company;
using JobApplicationTrackerAPI.DTOs.Response.Company;
using JobApplicationTrackerAPI.Models;
using JobApplicationTrackerAPI.Repository.Company;

namespace JobApplicationTrackerAPI.Service.Company
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Models.Company> Add(AddCompanyCommandDto companyDto)
        {
            var company = new Models.Company
            {
                Id = Guid.NewGuid(),
                Name = companyDto.Name,
                Description = companyDto.Description,
                ContactPerson = companyDto.ContactPerson,
                PhoneNumber = companyDto.PhoneNumber,
                CompanyURL = companyDto.CompanyURL
            };

            await _companyRepository.Add(company);
            return company;
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await _companyRepository.GetById(id);

            if (entity == null) return false;

            await _companyRepository.Delete(entity);

            return true;
        }

        public Task<bool> ExistByName(string name)
        {
            return _companyRepository.ExistByName(name);
        }

        public async Task<List<CompanyListResponseDto>> GetAll()
        {
            var companies = await _companyRepository.GetAll();

            var responseCompanies = companies.Select(c => new CompanyListResponseDto()
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ContactPerson = c.ContactPerson,
                PhoneNumber = c.PhoneNumber,
                CompanyURL = c.CompanyURL
            }).ToList();

            return responseCompanies;
        }

        public async Task<CompanyResponseDto> GetById(Guid id)
        {
            var company = await _companyRepository.GetById(id);
            if (company == null) return null;

            return new CompanyResponseDto()
            {
                Id = company.Id,
                Name = company.Name,
                Description = company.Description,
                ContactPerson = company.ContactPerson,
                PhoneNumber = company.PhoneNumber,
                CompanyURL = company.CompanyURL,
                JobApplications = company.JobApplications
            };
        }

        public async Task<Models.Company> Update(Guid guid, UpdateCompanyCommandDto companyDto)
        {
            var company = await _companyRepository.GetById(guid);

            if (company == null) return null;

            company.Description = companyDto.Description;
            company.ContactPerson = companyDto.ContactPerson;
            company.PhoneNumber = companyDto.PhoneNumber;
            company.CompanyURL = companyDto.CompanyURL;

            await _companyRepository.Update(company);
            return company;
        }
    }
}