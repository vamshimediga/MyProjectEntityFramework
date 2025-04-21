using BusinessLayer;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyProjectEntityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyTypeAPIController : ControllerBase
    {
        private readonly BusinessLayer.ICompanyTypeRepository _companyTypeRepository;

        public CompanyTypeAPIController(BusinessLayer.ICompanyTypeRepository companyTypeRepository)
        {
            _companyTypeRepository = companyTypeRepository;
        }

        // GET: api/CompanyTypeAPI
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _companyTypeRepository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/CompanyTypeAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _companyTypeRepository.GetByIdAsync(id);
            return Ok(result);
        }

        // POST: api/CompanyTypeAPI
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CompanyTypeDomainModel companyType)
        {
           

            var result = await _companyTypeRepository.InsertAsync(companyType);
            return Ok(result);
        }

        // PUT: api/CompanyTypeAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CompanyTypeDomainModel companyType)
        {

            var result = await _companyTypeRepository.UpdateAsync(companyType);
            return Ok(result);
        }

        // DELETE: api/CompanyTypeAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _companyTypeRepository.DeleteAsync(id);
            return Ok(result);
        }
    }
}
