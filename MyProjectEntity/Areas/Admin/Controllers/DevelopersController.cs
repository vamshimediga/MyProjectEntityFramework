using AutoMapper;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DevelopersController : Controller
    {
        private readonly ServiceLayer<DeveloperDomainModel> _developerService;
        private readonly IMapper _mapper;
        private readonly ApiService _apiService;

        public DevelopersController(ServiceLayer<DeveloperDomainModel> developerService, IMapper mapper, ApiService apiService)
        {
            _developerService = developerService;
            _mapper = mapper;
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var developerDtos = await _developerService.GetAllAsync(_apiService.GetApiUrl(ApiEndpoint.Developer));
            var developers = _mapper.Map<List<DeveloperViewModel>>(developerDtos);
            return View(developers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var developerDto = await _developerService.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Developer), id);
            var developer = _mapper.Map<DeveloperViewModel>(developerDto);
            return View(developer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeveloperViewModel developerViewModel)
        {
            try
            {
                var developer = _mapper.Map<DeveloperDomainModel>(developerViewModel);
                int id = await _developerService.AddAsync(_apiService.GetApiUrl(ApiEndpoint.Developer), developer);

                if (id > 0)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error creating developer: " + ex.Message);
            }

            return View(developerViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var developerDto = await _developerService.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Developer), id);
            var developer = _mapper.Map<DeveloperViewModel>(developerDto);
            return View(developer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DeveloperViewModel developerViewModel)
        {
            try
            {
                var developer = _mapper.Map<DeveloperDomainModel>(developerViewModel);
                bool isSuccess = await _developerService.UpdateAsync($"{_apiService.GetApiUrl(ApiEndpoint.Developer)}/{id}", developer);

                if (isSuccess)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error updating developer: " + ex.Message);
            }

            return View(developerViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var developerDto = await _developerService.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Developer), id);
            var developer = _mapper.Map<DeveloperViewModel>(developerDto);
            return View(developer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int DeveloperId)
        {
            bool isSuccess = await _developerService.DeleteAsync(_apiService.GetApiUrl(ApiEndpoint.Developer), DeveloperId);
            if (isSuccess)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error deleting developer.");
            return RedirectToAction(nameof(Delete), new { id = DeveloperId });
        }
    }
}
