using JobApplicationTrackerAPI.DTOs.Command.Company;
using JobApplicationTrackerAPI.Repository.Company;
using Microsoft.AspNetCore.Http.HttpResults;

namespace JobApplicationTrackerAPI.Service.Company
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Models.Company> Add(AddOrUpdateCompanyCommandDto companyDto)
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

        public async Task<Models.Company> Delete(Guid id)
        {
            var entity = await _companyRepository.GetById(id);

            await _companyRepository.Delete(entity);

            return entity;
        }

        public async Task<IEnumerable<Models.Company>> GetAll()
        {
            return await _companyRepository.GetAll();
        }

        public async Task<Models.Company> GetById(Guid id)
        {
            return await _companyRepository.GetById(id);
        }

        public async Task<Models.Company> Update(Guid guid, AddOrUpdateCompanyCommandDto companyDto)
        {
            var company = new Models.Company
            {
                Id = guid,
                Name = companyDto.Name,
                Description = companyDto.Description,
                ContactPerson = companyDto.ContactPerson,
                PhoneNumber = companyDto.PhoneNumber,
                CompanyURL = companyDto.CompanyURL
            };

            await _companyRepository.Update(company);

            return company;
        }
    }
}