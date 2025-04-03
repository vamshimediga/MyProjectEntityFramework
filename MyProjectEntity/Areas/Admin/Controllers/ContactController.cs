using AutoMapper;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        private readonly ServiceLayer<Contact> _service;
        private readonly IMapper _mapper;
        private readonly ApiService _apiService;

        public ContactController(ServiceLayer<Contact> service, IMapper mapper, ApiService apiService)
        {
            _service = service;
            _mapper = mapper;
            _apiService = apiService;
        }

        // GET: ContactController
        public async Task<IActionResult> Index()
        {
            List<Contact> contacts = await _service.GetAllAsync(_apiService.GetApiUrl(ApiEndpoint.Contact));
            List<ContactViewModel> contactsViewModel = _mapper.Map<List<ContactViewModel>>(contacts);
            return View(contactsViewModel);
        }

        // GET: ContactController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Contact contact = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Contact), id);
            ContactViewModel contactViewModel = _mapper.Map<ContactViewModel>(contact);
            return View(contactViewModel);
        }

        // GET: ContactController/Create
        public IActionResult Create()
        {
            ContactViewModel contactViewModel = new ContactViewModel();
            return View(contactViewModel);
        }

        // POST: ContactController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactViewModel contactViewModel)
        {
            try
            {
                Contact contact = _mapper.Map<Contact>(contactViewModel);
                int id = await _service.AddAsync(_apiService.GetApiUrl(ApiEndpoint.Contact), contact);

                if (id > 0)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error creating contact: " + ex.Message);
            }
            return View(contactViewModel);
        }

        // GET: ContactController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Contact contact = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Contact), id);
            ContactViewModel contactViewModel = _mapper.Map<ContactViewModel>(contact);
            return View(contactViewModel);
        }

        // POST: ContactController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContactViewModel contactViewModel)
        {
            try
            {
                Contact contact = _mapper.Map<Contact>(contactViewModel);
                bool isSuccess = await _service.UpdateAsync($"{_apiService.GetApiUrl(ApiEndpoint.Contact)}/{id}", contact);

                if (isSuccess)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error updating contact: " + ex.Message);
            }
            return View(contactViewModel);
        }

        // GET: ContactController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Contact contact = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Contact), id);
            ContactViewModel contactViewModel = _mapper.Map<ContactViewModel>(contact);
            return View(contactViewModel);
        }

        // POST: ContactController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ContactID)
        {
            bool isSuccess = await _service.DeleteAsync(_apiService.GetApiUrl(ApiEndpoint.Contact), ContactID);
            if (isSuccess)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error deleting contact.");
            return RedirectToAction(nameof(Delete), new { ContactID });
        }
    }
}
