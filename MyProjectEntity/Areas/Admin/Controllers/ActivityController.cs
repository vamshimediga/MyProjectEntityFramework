using AutoMapper;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] // ✅ Only Admin can access this controller
    public class ActivityController : Controller
    {
        private readonly ServiceLayer<Activity> _service;
        private readonly ServiceLayer<Opportunity> _serviceOpportunity;
        private readonly IMapper _mapper;
        private readonly ApiService _apiService;

        public ActivityController(ServiceLayer<Activity> service, IMapper mapper, ServiceLayer<Opportunity> serviceOpportunity, ApiService apiService)
        {
            _service = service;
            _mapper = mapper;
            _serviceOpportunity = serviceOpportunity;
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            List<Activity> activitiesDto = await _service.GetAllAsync(_apiService.GetApiUrl(ApiEndpoint.Activity));
            List<ActivityViewModel> viewModel = _mapper.Map<List<ActivityViewModel>>(activitiesDto);
            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            Activity activitiesDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Activity), id);
            ActivityViewModel viewModel = _mapper.Map<ActivityViewModel>(activitiesDto);
            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            ActivityViewModel activityViewModel = new ActivityViewModel();
            List<Opportunity> opportunitiesDto = await _serviceOpportunity.GetAllAsync(_apiService.GetApiUrl(ApiEndpoint.Opportunity));
            List<OpportunityViewModel> opportunitiesvm = _mapper.Map<List<OpportunityViewModel>>(opportunitiesDto);
            activityViewModel.opportunities = opportunitiesvm;
            return View(activityViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActivityViewModel activityViewModel)
        {
            try
            {
                var activity = _mapper.Map<Activity>(activityViewModel);
                int id = await _service.AddAsync(_apiService.GetApiUrl(ApiEndpoint.Activity), activity);

                if (id != null)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error creating activity: " + ex.Message);
            }
            return View(activityViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Activity activitiesDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Activity), id);
            ActivityViewModel viewModel = _mapper.Map<ActivityViewModel>(activitiesDto);
            List<Opportunity> opportunitiesDto = await _serviceOpportunity.GetAllAsync(_apiService.GetApiUrl(ApiEndpoint.Opportunity));
            List<OpportunityViewModel> opportunitiesvm = _mapper.Map<List<OpportunityViewModel>>(opportunitiesDto);
            viewModel.opportunities = opportunitiesvm;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ActivityViewModel activityViewModel)
        {
            try
            {
                var activity = _mapper.Map<Activity>(activityViewModel);
                bool isSuccess = await _service.UpdateAsync($"{_apiService.GetApiUrl(ApiEndpoint.Activity)}/{id}", activity);

                if (isSuccess)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error updating activity: " + ex.Message);
            }
            return View(activityViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Activity activitiesDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Activity), id);
            ActivityViewModel viewModel = _mapper.Map<ActivityViewModel>(activitiesDto);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ActivityID)
        {
            bool isSuccess = await _service.DeleteAsync(_apiService.GetApiUrl(ApiEndpoint.Activity), ActivityID);
            if (isSuccess)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error deleting activity.");
            return RedirectToAction(nameof(Delete), new { ActivityID });
        }
    }
}
