using BusinessLayer;
using BusinessLayer.implemation;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadAgentAPIController : ControllerBase
    {
        private readonly ILeadAgentRepository _leadAgentRepository;

        public LeadAgentAPIController(ILeadAgentRepository leadAgentRepository)
        {
            _leadAgentRepository = leadAgentRepository;
        }

        // GET: api/LeadAgentAPI
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var agents = await _leadAgentRepository.GetAll();
            return Ok(agents);
        }

        // GET: api/LeadAgentAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var agent = await _leadAgentRepository.GetByIdAsync(id);
            if (agent == null)
                return NotFound();

            return Ok(agent);
        }

        // POST: api/LeadAgentAPI
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LeadAgentDomainModel leadAgent)
        {
            if (leadAgent == null)
                return BadRequest();

            var result =await _leadAgentRepository.Add(leadAgent);
            return Ok(result);
        }

        // PUT: api/LeadAgentAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LeadAgentDomainModel leadAgent)
        {
            

           var result = await _leadAgentRepository.Update(leadAgent);
            return Ok(result);
        }

        // DELETE: api/LeadAgentAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            

         var result =   await _leadAgentRepository.Delete(id);
            return Ok(result);
        }
    }
}
