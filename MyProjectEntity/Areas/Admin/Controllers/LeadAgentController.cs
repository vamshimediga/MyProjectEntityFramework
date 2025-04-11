using AutoMapper;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class LeadAgentController : Controller
    {
        private readonly ServiceLayer<LeadAgentDomainModel> _service;
        private readonly IMapper _mapper;
        private readonly ApiService _apiService;

        public LeadAgentController(ServiceLayer<LeadAgentDomainModel> service, IMapper mapper, ApiService apiService)
        {
            _service = service;
            _mapper = mapper;
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            List<LeadAgentDomainModel> leadAgentsDto = await _service.GetAllAsync(_apiService.GetApiUrl(ApiEndpoint.LeadAgent));
            List<LeadAgentViewModel> viewModel = _mapper.Map<List<LeadAgentViewModel>>(leadAgentsDto);
            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            LeadAgentDomainModel leadAgentDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.LeadAgent), id);
            LeadAgentViewModel viewModel = _mapper.Map<LeadAgentViewModel>(leadAgentDto);
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View(new LeadAgentViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeadAgentViewModel viewModel)
        {
            try
            {
                LeadAgentDomainModel model = _mapper.Map<LeadAgentDomainModel>(viewModel);
                var id = await _service.AddAsync(_apiService.GetApiUrl(ApiEndpoint.LeadAgent), model);
                if (id > 0)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error creating Lead Agent: " + ex.Message);
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            LeadAgentDomainModel leadAgentDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.LeadAgent), id);
            LeadAgentViewModel viewModel = _mapper.Map<LeadAgentViewModel>(leadAgentDto);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeadAgentViewModel viewModel)
        {
            try
            {
                LeadAgentDomainModel model = _mapper.Map<LeadAgentDomainModel>(viewModel);
                var success = await _service.UpdateAsync($"{_apiService.GetApiUrl(ApiEndpoint.LeadAgent)}/{id}", model);
                if (success)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error updating Lead Agent: " + ex.Message);
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            LeadAgentDomainModel leadAgentDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.LeadAgent), id);
            LeadAgentViewModel viewModel = _mapper.Map<LeadAgentViewModel>(leadAgentDto);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int LeadAgentID)
        {
            var success = await _service.DeleteAsync(_apiService.GetApiUrl(ApiEndpoint.LeadAgent), LeadAgentID);
            if (success)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error deleting Lead Agent.");
            return RedirectToAction(nameof(Delete), new { id = LeadAgentID });
        }
    }
}
