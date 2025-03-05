using JobApplicationTrackerAPI.DTOs.Command.JobApplication;
using JobApplicationTrackerAPI.DTOs.Response.JobApplication;
using JobApplicationTrackerAPI.Service.JobApplicationService;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTrackerAPI.Controllers
{
    public class JobApplicationController : Controller
    {
        private readonly IJobApplicationService _jobApplicationService;

        public JobApplicationController(IJobApplicationService jobApplicationService)
        {
            _jobApplicationService = jobApplicationService;
        }

        public async Task<IActionResult> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet("/application/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var jobApp = await _jobApplicationService.GetById(id);
            if (jobApp == null) return NotFound();
            return Ok(jobApp);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddJobApplicationCommandDto addJobApplicationCommandDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Update(UpdateJobApplicationCommandDto updateDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Delete(Guid jobApplicationId)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> GetByCompanyId(Guid companyId)
        {
            throw new NotImplementedException();
        }
    }
}