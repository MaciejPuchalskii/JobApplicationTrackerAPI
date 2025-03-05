using JobApplicationTrackerAPI.DTOs.Command.JobApplication;
using JobApplicationTrackerAPI.Service.JobApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobApplicationTrackerAPI.Controllers
{
    [Route("api/jobapplications")]
    [Controller]
    [Authorize]
    public class JobApplicationController : Controller
    {
        private readonly IJobApplicationService _jobApplicationService;

        public JobApplicationController(IJobApplicationService jobApplicationService)
        {
            _jobApplicationService = jobApplicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var applications = await _jobApplicationService.GetAll();
            return Ok(applications);
        }

        [HttpGet("/application/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var jobApp = await _jobApplicationService.GetById(id);
            if (jobApp == null) return NotFound();
            return Ok(jobApp);
        }

        [HttpGet("{companyId}")]
        public async Task<IActionResult> GetByCompanyId(Guid companyId)
        {
            var applications = await _jobApplicationService.GetByCompanyId(companyId);
            return Ok(applications);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddJobApplicationCommandDto addJobApplicationCommandDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var createdJobApp = await _jobApplicationService.Add(addJobApplicationCommandDto, userId);

            return CreatedAtAction(nameof(GetById), new { id = createdJobApp.Id }, createdJobApp);
        }

        [HttpPut()]
        public async Task<IActionResult> Update(UpdateJobApplicationCommandDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedApplication = await _jobApplicationService.Update(updateDto);
            if (updatedApplication == null) return NotFound();
            return Ok(updatedApplication);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid jobApplicationId)
        {
            var result = await _jobApplicationService.Delete(jobApplicationId);
            if (!result) return NotFound();
            return Ok(result);
        }
    }
}