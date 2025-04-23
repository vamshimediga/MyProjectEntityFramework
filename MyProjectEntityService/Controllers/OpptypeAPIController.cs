using Microsoft.AspNetCore.Mvc;
using BusinessLayer.implemation;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer;

namespace MyProjectEntityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpptypeAPIController : ControllerBase
    {
        private readonly IOpptypeRepository _opptypeRepository;

        public OpptypeAPIController(IOpptypeRepository opptypeRepository)
        {
            _opptypeRepository = opptypeRepository;
        }

        // GET: api/OpptypeAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OpptypeDomainModel>>> Get()
        {
            var opptypes = await _opptypeRepository.Get();
            return Ok(opptypes);
        }

        // GET api/OpptypeAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OpptypeDomainModel>> Get(int id)
        {
            var opptype = await _opptypeRepository.GetOpptypeByIdAsync(id);
            return Ok(opptype);
        }

        // POST api/OpptypeAPI
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] OpptypeDomainModel model)
        {
            

            var result = await _opptypeRepository.InsertOpptypeAsync(model);
            return Ok(result);
        }
        // PUT api/AgentAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] OpptypeDomainModel model)
        {

            var updated = await _opptypeRepository.Update(model);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _opptypeRepository.DeleteAsync(id);
            return Ok(result);
        }

        // You can add PUT and DELETE logic later if needed.
    }
}
