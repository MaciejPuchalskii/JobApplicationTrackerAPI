using JobApplicationTrackerAPI.DTOs.Command.Company;
using JobApplicationTrackerAPI.DTOs.Response.Company;

namespace JobApplicationTrackerAPI.Service.Company
{
    public interface ICompanyService
    {
        Task<List<CompanyListResponseDto>> GetAll();

        Task<Models.Company> Add(AddCompanyCommandDto companyDto);

        Task<bool> Delete(Guid id);

        Task<CompanyResponseDto> GetById(Guid id);

        Task<Models.Company> Update(Guid guid, UpdateCompanyCommandDto companyDto);

        Task<bool> ExistByName(string name);
    }
}