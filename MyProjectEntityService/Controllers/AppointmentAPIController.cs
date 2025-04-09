using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer;
using Data.Repositories;
using Entities;

namespace MyProjectEntityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentAPIController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentAPIController(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        // GET: api/AppointmentAPI
        [HttpGet]
        public async Task<ActionResult<List<Appointment>>> GetAllAppointments()
        {
            var appointments = await _appointmentRepository.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        // GET api/AppointmentAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointmentById(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
           
            return Ok(appointment);
        }

        // POST api/AppointmentAPI
        [HttpPost]
        public async Task<ActionResult<int>> InsertAppointment([FromBody] Appointment appointment)
        {
           
            int newId = await _appointmentRepository.InsertAppointmentAsync(appointment);
            return Ok(newId);
        }

        // PUT api/AppointmentAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] Appointment appointment)
        {
            if (appointment == null || id != appointment.AppointmentID)
            {
                return BadRequest("Invalid request");
            }

            bool isUpdated = await _appointmentRepository.UpdateAppointmentAsync(appointment);
            return Ok(isUpdated);
        }

        // DELETE api/AppointmentAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            bool isDeleted = await _appointmentRepository.DeleteAppointmentAsync(id);
            
            return Ok(isDeleted);
        }


        [HttpPut("AddTocart/{id}")]
        public async Task<ActionResult> AddTocart(int id)
        {
            bool  appointment = await _appointmentRepository.AddToCartAsync(id);
            return Ok(appointment);
        }

    }
}
