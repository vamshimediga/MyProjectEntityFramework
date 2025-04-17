using AutoMapper;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TeamLeadController : Controller
    {
        private readonly ServiceLayer<TeamLead> _service;
        private readonly IMapper _mapper;
        private readonly ApiService _apiService;

        public TeamLeadController(ServiceLayer<TeamLead> service, IMapper mapper, ApiService apiService)
        {
            _service = service;
            _mapper = mapper;
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var leadsDto = await _service.GetAllAsync(_apiService.GetApiUrl(ApiEndpoint.TeamLead));
            var viewModel = _mapper.Map<List<TeamLeadViewModel>>(leadsDto);
            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var leadDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.TeamLead), id);
            var viewModel = _mapper.Map<TeamLeadViewModel>(leadDto);
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View(new TeamLeadViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamLeadViewModel viewModel)
        {
            try
            {
                var lead = _mapper.Map<TeamLead>(viewModel);
                int id = await _service.AddAsync(_apiService.GetApiUrl(ApiEndpoint.TeamLead), lead);
                if (id > 0)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error creating team lead: " + ex.Message);
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var leadDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.TeamLead), id);
            var viewModel = _mapper.Map<TeamLeadViewModel>(leadDto);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeamLeadViewModel viewModel)
        {
            try
            {
                var lead = _mapper.Map<TeamLead>(viewModel);
                bool result = await _service.UpdateAsync($"{_apiService.GetApiUrl(ApiEndpoint.TeamLead)}/{id}", lead);
                if (result)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error updating team lead: " + ex.Message);
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var leadDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.TeamLead), id);
            var viewModel = _mapper.Map<TeamLeadViewModel>(leadDto);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int teamLeadId)
        {
            bool result = await _service.DeleteAsync(_apiService.GetApiUrl(ApiEndpoint.TeamLead), teamLeadId);
            if (result)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error deleting team lead.");
            return RedirectToAction(nameof(Delete), new { id = teamLeadId });
        }
    }
}
