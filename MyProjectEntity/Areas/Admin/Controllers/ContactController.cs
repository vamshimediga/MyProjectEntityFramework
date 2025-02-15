using AutoMapper;
using BusinessLayer;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IContact _contact;
        private readonly IMapper _mapper;
        public ContactController(IContact contact,IMapper mapper)
        {
            _contact = contact;
            _mapper = mapper;
        }
        // GET: ContactController
        public async Task<ActionResult> Index()
        {
            List<Contact> contacts = await _contact.GetContacts(); 
            List<ContactViewModel> contactsViewModel = _mapper.Map<List<ContactViewModel>>(contacts); 
            return View(contactsViewModel);
        }

        // GET: ContactController/Details/5
        public async  Task<ActionResult> Details(int id)
        {
            Contact contact = await _contact.GetContactById(id);
            ContactViewModel contactViewModel = _mapper.Map<ContactViewModel>(contact); 
            return View(contactViewModel);
        }

        // GET: ContactController/Create
        public ActionResult Create()
        {
            ContactViewModel contact = new ContactViewModel();
            return View(contact);
        }

        // POST: ContactController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ContactViewModel contact)
        {
            try
            {
                Contact contactDATA= _mapper.Map<Contact>(contact); 
                bool B=await _contact.Insert(contactDATA);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContactController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Contact contact = await _contact.GetContactById(id);
            ContactViewModel contactViewModel = _mapper.Map<ContactViewModel>(contact);
            return View(contactViewModel);
        }

        // POST: ContactController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ContactViewModel contact)
        {
            try
            {

                Contact contactDATA = _mapper.Map<Contact>(contact);
                bool B = await _contact.Update(contactDATA);
                return RedirectToAction(nameof(Index));
             
            }
            catch
            {
                return View();
            }
        }

        // GET: ContactController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Contact contact = await _contact.GetContactById(id);
            ContactViewModel contactViewModel = _mapper.Map<ContactViewModel>(contact);
            return View(contactViewModel);
        }

        // POST: ContactController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                bool bb = await _contact.Delete(id);    
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
