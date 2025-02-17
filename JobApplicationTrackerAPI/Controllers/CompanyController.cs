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
            if (company == null) return NotFound();
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddCompanyCommandDto companyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exists = await _companyService.ExistByName(companyDto.Name);
            if (exists)
            {
                return Conflict("Company with this name already exists.");
            }

            var createdCompany = await _companyService.Add(companyDto);
            return CreatedAtAction(nameof(GetById), new { id = createdCompany.Id }, createdCompany);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCompanyCommandDto companyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedCompany = await _companyService.Update(id, companyDto);
            if (updatedCompany == null) return NotFound();
            return Ok(updatedCompany);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _companyService.Delete(id);
            if (!result) return NotFound();
            return Ok(new { message = "Successfully removed the company" });
        }
    }
}