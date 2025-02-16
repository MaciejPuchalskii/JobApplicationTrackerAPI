using JobApplicationTrackerAPI.DTOs.Command.Company;
using JobApplicationTrackerAPI.Service.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTrackerAPI.Controllers
{
    [Route("api/companies")]
    [Controller]
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _companyService.GetAll();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var company = await _companyService.GetById(id);
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddOrUpdateCompanyCommandDto companyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCompany = await _companyService.Add(companyDto);
            return CreatedAtAction(nameof(GetById), new { id = createdCompany.Id }, createdCompany);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AddOrUpdateCompanyCommandDto companyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCompany = await _companyService.Add(companyDto);
            return CreatedAtAction(nameof(GetById), new { id = createdCompany.Id }, createdCompany);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = _companyService.Delete(id);
            return Ok(result);
        }
    }
}