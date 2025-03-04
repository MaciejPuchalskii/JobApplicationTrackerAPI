using JobApplicationTrackerAPI.DTOs.Command.JobApplication;
using JobApplicationTrackerAPI.DTOs.Response.JobApplication;
using JobApplicationTrackerAPI.Models;
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

        public async Task<JobApplicationAddResponseDto> Add(AddJobApplicationCommandDto addJobApplicationDto, string userId)
        {
            var jobApplication = new JobApplication()
            {
                Id = new Guid(),
                AppliedDate = addJobApplicationDto.AppliedDate,
                CompanyId = addJobApplicationDto.CompanyId,
                Description = addJobApplicationDto.Description,
                JobAdvertURL = addJobApplicationDto.JobAdvertURL,
                Position = addJobApplicationDto.Position,
                Status = addJobApplicationDto.Status,
                UserId = userId
            };

            _jobApplicationRepository.Add(jobApplication);

            return new JobApplicationAddResponseDto()
            {
                Id = jobApplication.Id,
                AppliedDate = jobApplication.AppliedDate,
                Position = jobApplication.Position,
            };
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