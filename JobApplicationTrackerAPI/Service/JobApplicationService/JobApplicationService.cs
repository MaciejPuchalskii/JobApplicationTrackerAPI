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

        public async Task<bool> Delete(Guid id)
        {
            var entity = await _jobApplicationRepository.GetById(id);
            if (entity == null) return false;

            await _jobApplicationRepository.Delete(entity);

            return true;
        }

        public async Task<List<JobApplicationGetAllResponseDto>> GetAll()
        {
            var applications = await _jobApplicationRepository.GetAll();

            return applications.Select(app => new JobApplicationGetAllResponseDto()
            {
                Position = app.Position,
                AppliedDate = app.AppliedDate,
                CompanyId = app.CompanyId,
                Description = app.Description,
                Id = app.Id,
                Status = app.Status
            }).ToList();
        }

        public async Task<List<JobApplicationGetAllResponseDto>> GetByCompanyId(Guid companyId)
        {
            var jobApps = await _jobApplicationRepository.GetByCompanyId(companyId);
            if (jobApps == null)
            {
                throw new Exception("Invalid company id");
            }
            return jobApps.Select(app => new JobApplicationGetAllResponseDto()
            {
                Position = app.Position,
                AppliedDate = app.AppliedDate,
                CompanyId = app.CompanyId,
                Description = app.Description,
                Id = app.Id,
                Status = app.Status
            }).ToList();
        }

        public async Task<JobApplicationResponseDto> GetById(Guid id)
        {
            var jobApplication = await _jobApplicationRepository.GetById(id);
            if (jobApplication == null) return null;

            return new JobApplicationResponseDto()
            {
                Id = jobApplication.Id,
                Position = jobApplication.Position,
                Description = jobApplication.Description,
                Status = jobApplication.Status,
                AppliedDate = jobApplication.AppliedDate,
                JobAdvertURL = jobApplication.JobAdvertURL,
                CompanyId = jobApplication.CompanyId,
                NotesList = jobApplication.NotesList?.ToList(),
                Attachments = jobApplication.Attachments?.ToList()
            };
        }

        public async Task<JobApplicationResponseDto> Update(UpdateJobApplicationCommandDto updateDto)
        {
            var jobApplication = await _jobApplicationRepository.GetById(updateDto.Id);

            if (jobApplication == null) return null;

            jobApplication.Description = updateDto.Description;
            jobApplication.AppliedDate = updateDto.AppliedDate;
            jobApplication.Status = updateDto.Status;
            jobApplication.Position = updateDto.Position;
            jobApplication.JobAdvertURL = updateDto.JobAdvertURL;
            jobApplication.CompanyId = updateDto.CompanyId;

            await _jobApplicationRepository.Update(jobApplication);

            return new JobApplicationResponseDto()
            {
                Id = jobApplication.Id,
                Position = jobApplication.Position,
                Description = jobApplication.Description,
                Status = jobApplication.Status,
                AppliedDate = jobApplication.AppliedDate,
                JobAdvertURL = jobApplication.JobAdvertURL,
                CompanyId = jobApplication.CompanyId,
                NotesList = jobApplication.NotesList?.ToList(),
                Attachments = jobApplication.Attachments?.ToList()
            };
        }
    }
}