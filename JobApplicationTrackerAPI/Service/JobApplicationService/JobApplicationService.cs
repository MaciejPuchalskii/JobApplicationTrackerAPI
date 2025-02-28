using JobApplicationTrackerAPI.DTOs.Command.JobApplication;
using JobApplicationTrackerAPI.DTOs.Response.JobApplication;
using JobApplicationTrackerAPI.Repository.JobApplication;

namespace JobApplicationTrackerAPI.Service.JobApplicationService
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;

        public JobApplicationService(IJobApplicationRepository jobApplicationRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
        }

        public Task<JobApplicationResponseDto> Add(AddJobApplicationCommandDto addJobApplicationDto, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid jobApplicationId)
        {
            throw new NotImplementedException();
        }

        public Task<List<JobApplicationGetAllResponseDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<JobApplicationGetAllResponseDto>> GetByCompanyId(Guid companyId)
        {
            throw new NotImplementedException();
        }

        public Task<JobApplicationResponseDto> Update(UpdateJobApplicationCommandDto updateDto)
        {
            throw new NotImplementedException();
        }
    }
}