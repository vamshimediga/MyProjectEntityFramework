using AutoMapper;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AgentController : Controller
    {
        private readonly ServiceLayer<AgentDomainModel> _service;
        private readonly IMapper _mapper;
        private readonly ApiService _apiService;

        public AgentController(ServiceLayer<AgentDomainModel> service, IMapper mapper, ApiService apiService)
        {
            _service = service;
            _mapper = mapper;
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
           
            return View();
        }
        public async Task<JsonResult> GetAllAgents()
        {
            List<AgentDomainModel> agentsDto = await _service.GetAllAsync(_apiService.GetApiUrl(ApiEndpoint.Agent));
            List<AgentViewModel> viewModel = _mapper.Map<List<AgentViewModel>>(agentsDto);
            return Json(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            AgentDomainModel agentDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Agent), id);
            AgentViewModel viewModel = _mapper.Map<AgentViewModel>(agentDto);
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View(new AgentViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AgentViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            try
            {
                AgentDomainModel agent = _mapper.Map<AgentDomainModel>(viewModel);
                var id = await _service.AddAsync(_apiService.GetApiUrl(ApiEndpoint.Agent), agent);
                if (id > 0)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error creating agent: " + ex.Message);
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            AgentDomainModel agentDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Agent), id);
            AgentViewModel viewModel = _mapper.Map<AgentViewModel>(agentDto);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AgentViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            try
            {
                AgentDomainModel agent = _mapper.Map<AgentDomainModel>(viewModel);
                var isSuccess = await _service.UpdateAsync($"{_apiService.GetApiUrl(ApiEndpoint.Agent)}/{id}", agent);
                if (isSuccess)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error updating agent: " + ex.Message);
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            AgentDomainModel agentDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Agent), id);
            AgentViewModel viewModel = _mapper.Map<AgentViewModel>(agentDto);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int AgentID)
        {
            var isSuccess = await _service.DeleteAsync(_apiService.GetApiUrl(ApiEndpoint.Agent), AgentID);
            if (isSuccess)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error deleting agent.");
            return RedirectToAction(nameof(Delete), new { id = AgentID });
        }
    }
}
