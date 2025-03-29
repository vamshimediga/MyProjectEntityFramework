using AutoMapper;
using BusinessLayer;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyProjectEntityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class ActivityAPIController : ControllerBase
    {
        private readonly IActivity _activity;

        public ActivityAPIController(IActivity activity)
        {
            _activity = activity;
        }

        // GET: api/ActivityAPI
        [HttpGet]
        public async Task<ActionResult<List<Activity>>> Get()
        {
            var activities = await _activity.GetAllActivitiesAsync();
            return Ok(activities);
        }

        // GET api/ActivityAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> Get(int id)
        {
            var activity = await _activity.GetActivityByIdAsync(id);
            if (activity == null)
                return NotFound();

            return Ok(activity);
        }

        // POST api/ActivityAPI
        [HttpPost]
        public async Task<ActionResult<int>> Post(Activity activity)
        {
            int id = await _activity.InsertActivityAsync(activity);
            return CreatedAtAction(nameof(Get), new { id }, id);
        }

        // PUT api/ActivityAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Activity activity)
        {
            if (id != activity.ActivityID)
                return BadRequest();

            bool updated = await _activity.UpdateActivityAsync(activity);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE api/ActivityAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _activity.DeleteActivityAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }

}
