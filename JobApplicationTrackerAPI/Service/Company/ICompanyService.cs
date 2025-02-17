﻿using JobApplicationTrackerAPI.DTOs.Command.Company;

namespace JobApplicationTrackerAPI.Service.Company
{
    public interface ICompanyService
    {
        Task<IEnumerable<Models.Company>> GetAll();

        Task<Models.Company> Add(AddCompanyCommandDto companyDto);

        Task<Models.Company> Delete(Guid id);

        Task<Models.Company> GetById(Guid id);

        Task<Models.Company> Update(Guid guid, UpdateCompanyCommandDto companyDto);

        Task<bool> ExistByName(string name);
    }
}