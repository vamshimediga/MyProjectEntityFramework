using BusinessLayer;
using BusinessLayer.implemation;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyProjectEntityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactAPIController : ControllerBase
    {
        private readonly IContact _contactRepository;

        public ContactAPIController(IContact contactRepository)
        {
            _contactRepository = contactRepository;
        }

        // GET: api/ContactAPI
        [HttpGet]
        public async Task<ActionResult<List<Contact>>> GetContacts()
        {
            List<Contact> contacts = await _contactRepository.GetContacts();
            return Ok(contacts);
        }

        // GET api/ContactAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contact = await _contactRepository.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        // POST api/ContactAPI
        [HttpPost]
        public async Task<ActionResult> CreateContact([FromBody] Contact contact)
        {
            if (contact == null)
            {
                return BadRequest("Invalid contact data.");
            }

            var result = await _contactRepository.Insert(contact);
            return Ok(result);
        }

        // PUT api/ContactAPI/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateContact(int id, [FromBody] Contact contact)
        {
            if (contact == null || contact.ContactID != id)
            {
                return BadRequest("Invalid contact data.");
            }

            var existingContact = await _contactRepository.GetContactById(id);
            if (existingContact == null)
            {
                return NotFound();
            }

            var result = await _contactRepository.Update(contact);
            if (result)
            {
                return NoContent();
            }

            return BadRequest("Failed to update contact.");
        }

        // DELETE api/ContactAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteContact(int id)
        {
            var contact = await _contactRepository.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }

            var result = await _contactRepository.Delete(id);
            if (result)
            {
                return NoContent();
            }

            return BadRequest("Failed to delete contact.");
        }
    }
}
