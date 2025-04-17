using BusinessLayer;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyProjectEntityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamLeadAPIController : ControllerBase
    {
        private readonly ITeamLeadRepository _teamLeadRepo;

        public TeamLeadAPIController(ITeamLeadRepository teamLeadRepo)
        {
            _teamLeadRepo = teamLeadRepo;
        }

        // GET: api/TeamLeadAPI
        [HttpGet]
        public async Task<ActionResult<List<TeamLead>>> Get()
        {
            var leads = await _teamLeadRepo.GetAllTeamLeadsAsync();
            return Ok(leads);
        }

        // GET api/TeamLeadAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamLead>> Get(int id)
        {
            var lead = await _teamLeadRepo.GetTeamLeadByIdAsync(id);
            
            return Ok(lead);
        }

        // POST api/TeamLeadAPI
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TeamLead teamLead)
        {
            var result = await _teamLeadRepo.AddTeamLeadAsync(teamLead);
            return Ok(result);

        }

        // PUT api/TeamLeadAPI/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TeamLead teamLead)
        {
           

            var result = await _teamLeadRepo.UpdateTeamLeadAsync(teamLead);
            return Ok(result);
        }

        // DELETE api/TeamLeadAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _teamLeadRepo.DeleteTeamLeadAsync(id);
            return Ok(result);
        }
    }
}
