using AutoMapper;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PatientController : Controller
    {
        private readonly ServiceLayer<Patient> _service;
        private readonly IMapper _mapper;
        private readonly ApiService _apiService;

        public PatientController(ServiceLayer<Patient> service, IMapper mapper, ApiService apiService)
        {
            _service = service;
            _mapper = mapper;
            _apiService = apiService;
        }

        // GET: PatientController
        public async Task<IActionResult> Index()
        {
            List<Patient> patients = await _service.GetAllAsync(_apiService.GetApiUrl(ApiEndpoint.Patient));
            List<PatientViewModel> viewModel = _mapper.Map<List<PatientViewModel>>(patients);
            return View(viewModel);
        }

        // GET: PatientController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Patient patient = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Patient), id);
            PatientViewModel viewModel = _mapper.Map<PatientViewModel>(patient);
            return View(viewModel);
        }

        // GET: PatientController/Create
        public IActionResult Create()
        {
            return View(new PatientViewModel());
        }

        // POST: PatientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientViewModel patientViewModel)
        {
            try
            {
                Patient patient = _mapper.Map<Patient>(patientViewModel);
                int id = await _service.AddAsync(_apiService.GetApiUrl(ApiEndpoint.Patient), patient);

                if (id != 0)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error creating patient: " + ex.Message);
            }
            return View(patientViewModel);
        }

        // GET: PatientController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Patient patient = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Patient), id);
            PatientViewModel viewModel = _mapper.Map<PatientViewModel>(patient);
            return View(viewModel);
        }

        // POST: PatientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PatientViewModel patientViewModel)
        {
            try
            {
                Patient patient = _mapper.Map<Patient>(patientViewModel);
                bool isSuccess = await _service.UpdateAsync($"{_apiService.GetApiUrl(ApiEndpoint.Patient)}/{id}", patient);

                if (isSuccess)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error updating patient: " + ex.Message);
            }
            return View(patientViewModel);
        }

        // GET: PatientController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Patient patient = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Patient), id);
            PatientViewModel viewModel = _mapper.Map<PatientViewModel>(patient);
            return View(viewModel);
        }

        // POST: PatientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int PatientID)
        {
            bool isSuccess = await _service.DeleteAsync(_apiService.GetApiUrl(ApiEndpoint.Patient), PatientID);
            if (isSuccess)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error deleting patient.");
            return RedirectToAction(nameof(Delete), new { id = PatientID });
        }
    }
}
