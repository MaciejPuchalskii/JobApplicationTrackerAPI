using JobApplicationTrackerAPI.DTOs.Command.JobApplication;
using JobApplicationTrackerAPI.DTOs.Response.JobApplication;

namespace JobApplicationTrackerAPI.Service.JobApplicationService
{
    public interface IJobApplicationService
    {
        Task<JobApplicationResponseDto> Add(AddJobApplicationCommandDto addJobApplicationDto, string userId);

        Task<bool> Delete(Guid jobApplicationId);

        Task<List<JobApplicationGetAllResponseDto>> GetAll();

        Task<List<JobApplicationGetAllResponseDto>> GetByCompanyId(Guid companyId);

        Task<JobApplicationResponseDto> Update(UpdateJobApplicationCommandDto updateDto);
    }
}