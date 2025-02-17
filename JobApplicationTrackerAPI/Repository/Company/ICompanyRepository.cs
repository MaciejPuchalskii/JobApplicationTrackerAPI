namespace JobApplicationTrackerAPI.Repository.Company
{
    public interface ICompanyRepository : IGenericRepository<Models.Company>
    {
        Task<bool> ExistByName(string name);
    }
}