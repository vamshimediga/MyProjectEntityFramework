using AutoMapper;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserProfileController : Controller
    {
        private readonly ServiceLayer<UserProfile> _service;
        private readonly IMapper _mapper;
        private readonly ApiService _apiService;

        public UserProfileController(ServiceLayer<UserProfile> service, IMapper mapper, ApiService apiService)
        {
            _service = service;
            _mapper = mapper;
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var userProfilesDto = await _service.GetAllAsync(_apiService.GetApiUrl(ApiEndpoint.UserProfile));
            var viewModel = _mapper.Map<List<UserProfileViewModel>>(userProfilesDto);
            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var userProfileDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.UserProfile), id);
            var viewModel = _mapper.Map<UserProfileViewModel>(userProfileDto);
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View(new UserProfileViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserProfileViewModel userProfileViewModel)
        {
            try
            {
                var entity = _mapper.Map<UserProfile>(userProfileViewModel);
                int id = await _service.AddAsync(_apiService.GetApiUrl(ApiEndpoint.UserProfile), entity);

                if (id != 0)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error creating user profile: " + ex.Message);
            }

            return View(userProfileViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var userProfileDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.UserProfile), id);
            var viewModel = _mapper.Map<UserProfileViewModel>(userProfileDto);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserProfileViewModel userProfileViewModel)
        {
            try
            {
                var entity = _mapper.Map<UserProfile>(userProfileViewModel);
                var isSuccess = await _service.UpdateAsync($"{_apiService.GetApiUrl(ApiEndpoint.UserProfile)}/{id}", entity);

                if (isSuccess)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error updating user profile: " + ex.Message);
            }

            return View(userProfileViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var userProfileDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.UserProfile), id);
            var viewModel = _mapper.Map<UserProfileViewModel>(userProfileDto);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int UserProfileID)
        {
            var isSuccess = await _service.DeleteAsync(_apiService.GetApiUrl(ApiEndpoint.UserProfile), UserProfileID);

            if (isSuccess)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error deleting user profile.");
            return RedirectToAction(nameof(Delete), new { id = UserProfileID });
        }
    }
}
