using JobApplicationTrackerAPI.Models;

namespace JobApplicationTrackerAPI.Repository.JobApplication
{
    public interface IJobApplicationRepository : IGenericRepository<Models.JobApplication>
    {
        Task<List<Models.JobApplication>> GetByCompanyId(Guid companyId);
    }
}