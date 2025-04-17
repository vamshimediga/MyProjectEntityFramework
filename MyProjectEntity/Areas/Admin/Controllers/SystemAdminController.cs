using AutoMapper;
using BusinessLayer;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SystemAdminController : Controller
    {
        private readonly ServiceLayer<SystemAdminDomainModel> _service;
        private readonly IMapper _mapper;
        private readonly ApiService _apiService;
        private readonly ServiceLayer<DeveloperDomainModel> _devservice;
        public SystemAdminController(ServiceLayer<SystemAdminDomainModel> service, IMapper mapper, ApiService apiService, ServiceLayer<DeveloperDomainModel> devservice)
        {
            _service = service;
            _mapper = mapper;
            _apiService = apiService;
            _devservice = devservice;
        }

        public async Task<IActionResult> Index()
        {
            List<SystemAdminDomainModel> dtoList = await _service.GetAllAsync(_apiService.GetApiUrl(ApiEndpoint.SystemAdmin));
            List<SystemAdminViewModel> viewModel = _mapper.Map<List<SystemAdminViewModel>>(dtoList);
            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            SystemAdminDomainModel dto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.SystemAdmin), id);
            SystemAdminViewModel viewModel = _mapper.Map<SystemAdminViewModel>(dto);
            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            List<DeveloperDomainModel> developerDomainModels = await _devservice.GetAllAsync(_apiService.GetApiUrl(ApiEndpoint.Developer));
            List<DeveloperViewModel> ViewModel = _mapper.Map<List<DeveloperViewModel>>(developerDomainModels);
            SystemAdminViewModel systemAdminViewModel = new SystemAdminViewModel();
            systemAdminViewModel.Developers= ViewModel;
            return View(systemAdminViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SystemAdminViewModel viewModel)
        {
            try
            {
                SystemAdminDomainModel dto = _mapper.Map<SystemAdminDomainModel>(viewModel);
                int id = await _service.AddAsync(_apiService.GetApiUrl(ApiEndpoint.SystemAdmin), dto);

                if (id > 0)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error creating system admin: " + ex.Message);
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            SystemAdminDomainModel dto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.SystemAdmin), id);
            SystemAdminViewModel viewModel = _mapper.Map<SystemAdminViewModel>(dto);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SystemAdminViewModel viewModel)
        {
            try
            {
                SystemAdminDomainModel dto = _mapper.Map<SystemAdminDomainModel>(viewModel);
                bool updated = await _service.UpdateAsync($"{_apiService.GetApiUrl(ApiEndpoint.SystemAdmin)}/{id}", dto);

                if (updated)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error updating system admin: " + ex.Message);
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            SystemAdminDomainModel dto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.SystemAdmin), id);
            SystemAdminViewModel viewModel = _mapper.Map<SystemAdminViewModel>(dto);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int SystemAdminId)
        {
            bool deleted = await _service.DeleteAsync(_apiService.GetApiUrl(ApiEndpoint.SystemAdmin), SystemAdminId);
            if (deleted)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error deleting system admin.");
            return RedirectToAction(nameof(Delete), new { id = SystemAdminId });
        }
    }
}
