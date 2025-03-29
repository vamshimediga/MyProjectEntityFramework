using BusinessLayer;
using Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyProjectEntityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpportunityAPIController : ControllerBase
    {
        private readonly IOpportunity _opportunityRepository;

        public OpportunityAPIController(IOpportunity opportunityRepository)
        {
            _opportunityRepository = opportunityRepository;
        }

        // ✅ Get all opportunities
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var opportunities = await _opportunityRepository.GetAllOpportunitiesAsync();
            return Ok(opportunities);
        }

        // ✅ Get opportunity by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var opportunity = await _opportunityRepository.GetOpportunityByIdAsync(id);
            if (opportunity == null)
            {
                return NotFound("Opportunity not found.");
            }
            return Ok(opportunity);
        }

        // ✅ Insert new opportunity
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Opportunity opportunity)
        {
            if (opportunity == null)
            {
                return BadRequest("Invalid opportunity data.");
            }

            var newId = await _opportunityRepository.InsertOpportunityAsync(opportunity);
            return Ok(newId);
        }

        // ✅ Update opportunity
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Opportunity opportunity)
        {
            if (opportunity == null || opportunity.OpportunityID != id)
            {
                return BadRequest("Invalid opportunity data.");
            }

            var isUpdated = await _opportunityRepository.UpdateOpportunityAsync(opportunity);
            return isUpdated ? Ok(new { Message = "Opportunity updated successfully" }) : NotFound("Opportunity not found.");
        }

        // ✅ Delete opportunity
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _opportunityRepository.DeleteOpportunityAsync(id);
            return isDeleted ? Ok(true) : Ok(false);
        }
    }

}
