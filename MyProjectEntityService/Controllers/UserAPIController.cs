using BusinessLayer;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MyProjectEntityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private readonly IUsers _usersRepository;

        public UserAPIController(IUsers usersRepository)
        {
            _usersRepository = usersRepository;
        }

        // GET: api/UserAPI
        [HttpGet]
        public async Task<ActionResult<List<UserDomainModel>>> GetUsers()
        {
            var users = await _usersRepository.GetUsers();
            return Ok(users);
        }

        // GET api/UserAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDomainModel>> GetUserById(int id)
        {
            var user = await _usersRepository.GetUsersByid(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // POST api/UserAPI
        [HttpPost]
        public async Task<ActionResult<int>> InsertUser([FromBody] UserDomainModel model)
        {
            var userId = await _usersRepository.insert(model);
            return Ok(userId);
        }

        // PUT api/UserAPI
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserDomainModel model)
        {
            var result = await _usersRepository.update(model);
            if (!result)
                return BadRequest("Update failed.");
            return Ok(result);
        }

        // DELETE api/UserAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteUser(int id)
        {
            var result = await _usersRepository.delete(id);
            if (result == 0)
                return NotFound("User not found.");
            return Ok(result);
        }
    }
}
