using BusinessLayer;
using BusinessLayer.implemation;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        // GET: api/Patient
        [HttpGet]
        public async Task<ActionResult<List<Patient>>> Get()
        {
            var patients = await _patientRepository.GetAllAsync();
            return Ok(patients);
        }

        // GET api/Patient/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> Get(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null)
                return NotFound();
            return Ok(patient);
        }

        // POST api/Patient
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Patient patient)
        {
            var result = await _patientRepository.InsertAsync(patient);
            return Ok(result);
        }

        // PUT api/Patient/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Patient patient)
        {
            patient.PatientID = id;
            var result = await _patientRepository.UpdateAsync(patient);
            return Ok(result);
        }

        // DELETE api/Patient/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _patientRepository.DeleteAsync(id);
            return Ok(result);
        }
    }
}
