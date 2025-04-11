using BusinessLayer;
using Entities;
using Microsoft.AspNetCore.Mvc;


namespace MyProjectEntityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentAPIController : ControllerBase
    {
        private readonly IAgentRepository _agentRepository;

        public AgentAPIController(IAgentRepository agentRepository)
        {
            _agentRepository = agentRepository;
        }

        // GET: api/AgentAPI
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var agents = await _agentRepository.GetAllAsync();
            return Ok(agents);
        }

        // GET api/AgentAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var agent = await _agentRepository.GetByIdAsync(id);
           
            return Ok(agent);
        }

        // POST api/AgentAPI
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AgentDomainModel agent)
        {
            
            var createdAgent = await _agentRepository.InsertAsync(agent);
            return Ok(createdAgent);
        }

        // PUT api/AgentAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AgentDomainModel agent)
        {
           
            var updated = await _agentRepository.UpdateAsync(agent);
            return Ok(updated);
        }

        // DELETE api/AgentAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _agentRepository.DeleteAsync(id);
            Ok(success);
            return NoContent();
        }
    }
}
