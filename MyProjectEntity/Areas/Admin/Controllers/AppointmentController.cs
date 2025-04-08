using AutoMapper;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppointmentController : Controller
    {
        private readonly ServiceLayer<Appointment> _service;
        private readonly ServiceLayer<Patient> _patientService;
        private readonly IMapper _mapper;
        private readonly ApiService _apiService;

        public AppointmentController(ServiceLayer<Appointment> service, IMapper mapper, ApiService apiService, ServiceLayer<Patient> patientService)
        {
            _service = service;
            _mapper = mapper;
            _apiService = apiService;
            _patientService = patientService;
        }

        // GET: AppointmentController
        public async Task<IActionResult> Index()
        {
            List<Appointment> appointmentsDto = await _service.GetAllAsync(_apiService.GetApiUrl(ApiEndpoint.Appointment));
            List<AppointmentViewModel> viewModel = _mapper.Map<List<AppointmentViewModel>>(appointmentsDto);
            return View(viewModel);
        }

        // GET: AppointmentController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Appointment appointmentDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Appointment), id);
            AppointmentViewModel viewModel = _mapper.Map<AppointmentViewModel>(appointmentDto);
            return View(viewModel);
        }

        // GET: AppointmentController/Create
        public async Task<IActionResult> Create()
        {
            AppointmentViewModel appointmentViewModel = new AppointmentViewModel();
            List<Patient> patient = await _patientService.GetAllAsync(_apiService.GetApiUrl(ApiEndpoint.Patient));
            List<PatientViewModel> patientViewModels = _mapper.Map<List<PatientViewModel>>(patient);
            appointmentViewModel.Patients = patientViewModels;
            return View(appointmentViewModel);
        }

        // POST: AppointmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentViewModel appointmentViewModel)
        {
            try
            {
                var appointment = _mapper.Map<Appointment>(appointmentViewModel);
                int id = await _service.AddAsync(_apiService.GetApiUrl(ApiEndpoint.Appointment), appointment);

                if (id != 0)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error creating appointment: " + ex.Message);
            }
            return View(appointmentViewModel);
        }

        // GET: AppointmentController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Appointment appointmentDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Appointment), id);
            AppointmentViewModel viewModel = _mapper.Map<AppointmentViewModel>(appointmentDto);
            List<Patient> patient = await _patientService.GetAllAsync(_apiService.GetApiUrl(ApiEndpoint.Patient));
            List<PatientViewModel> patientViewModels = _mapper.Map<List<PatientViewModel>>(patient);
            viewModel.Patients = patientViewModels;
            return View(viewModel);
        }

        // POST: AppointmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AppointmentViewModel appointmentViewModel)
        {
            try
            {
                var appointment = _mapper.Map<Appointment>(appointmentViewModel);
                bool isSuccess = await _service.UpdateAsync($"{_apiService.GetApiUrl(ApiEndpoint.Appointment)}/{id}", appointment);

                if (isSuccess)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error updating appointment: " + ex.Message);
            }
            return View(appointmentViewModel);
        }

        // GET: AppointmentController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Appointment appointmentDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Appointment), id);
            AppointmentViewModel viewModel = _mapper.Map<AppointmentViewModel>(appointmentDto);
            return View(viewModel);
        }

        // POST: AppointmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int AppointmentID)
        {
            bool isSuccess = await _service.DeleteAsync(_apiService.GetApiUrl(ApiEndpoint.Appointment), AppointmentID);
            if (isSuccess)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error deleting appointment.");
            return RedirectToAction(nameof(Delete), new { AppointmentID });
        }
    }
}
