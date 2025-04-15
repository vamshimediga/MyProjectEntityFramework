using BusinessLayer;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyProjectEntityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperAPIController : ControllerBase
    {
        private readonly IDeveloperRepository _repository;

        public DeveloperAPIController(IDeveloperRepository repository)
        {
            _repository = repository;
        }

        // GET: api/DeveloperAPI
        [HttpGet]
        public async Task<ActionResult<List<DeveloperDomainModel>>> Get()
        {
            var developers = await _repository.GetAll();
            return Ok(developers);
        }

        // GET: api/DeveloperAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeveloperDomainModel>> Get(int id)
        {
            var developer = await _repository.GetById(id);
            if (developer == null)
                return NotFound();

            return Ok(developer);
        }

        // POST: api/DeveloperAPI
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] DeveloperDomainModel developer)
        {
            if (developer == null)
                return BadRequest();

            var newId = await _repository.Add(developer);
            return CreatedAtAction(nameof(Get), new { id = developer.DeveloperId }, newId);
        }

        // PUT: api/DeveloperAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DeveloperDomainModel developer)
        {
            if (id != developer.DeveloperId)
                return BadRequest("Developer ID mismatch.");

            var updated = await _repository.Update(developer);
            if (!updated)
                return NotFound();
            return Ok(true);
        }

        // DELETE: api/DeveloperAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.Delete(id);
            if (!deleted)
                return NotFound();

            return Ok(true);
        }
    }
}
