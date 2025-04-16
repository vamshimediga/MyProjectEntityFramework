using BusinessLayer.implemation;
using BusinessLayer;
using Data.Repositories;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemAdminAPIController : ControllerBase
    {
        private readonly ISystemAdmin _repository;

        public SystemAdminAPIController(ISystemAdmin repository)
        {
            _repository = repository;
        }

        // GET: api/SystemAdminAPI
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var admins = await _repository.GetAllAsync();
            return Ok(admins);
        }

        // GET api/SystemAdminAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var admin = await _repository.GetByIdAsync(id);
            return Ok(admin);
        }

        // POST api/SystemAdminAPI
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SystemAdminDomainModel admin)
        {
            
            var result = await _repository.InsertAsync(admin);
            return Ok(result);
        }

        // PUT api/SystemAdminAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SystemAdminDomainModel admin)
        {
            

            var result = await _repository.UpdateAsync(admin);
            return Ok(result);
        }

        // DELETE api/SystemAdminAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);
            return  Ok(result);
        }
    }
}
