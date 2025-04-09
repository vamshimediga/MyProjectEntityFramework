using Entities;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.implemation;
using BusinessLayer; // Namespace where your IUserProfileRepository is defined

namespace MyProjectEntityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileAPIController : ControllerBase
    {
        private readonly IUserProfileRepository _repository;

        public UserProfileAPIController(IUserProfileRepository repository)
        {
            _repository = repository;
        }

        // GET: api/UserProfileAPI
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var profiles = await _repository.GetAllAsync();
            return Ok(profiles);
        }

        // GET api/UserProfileAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var profile = await _repository.GetByIdAsync(id);
            return Ok(profile);
        }

        // POST api/UserProfileAPI
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserProfile userProfile)
        {
           
            var result = await _repository.AddAsync(userProfile);
            return Ok(result);
        }

        // PUT api/UserProfileAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserProfile userProfile)
        {
            var success = await _repository.UpdateAsync(userProfile);  
            return Ok(success);
        }

        // DELETE api/UserProfileAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repository.DeleteAsync(id);
            return Ok(success);
        }
    }
}
