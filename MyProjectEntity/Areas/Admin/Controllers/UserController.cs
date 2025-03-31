using AutoMapper;
using BusinessLayer;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly ServiceLayer<UserDomainModel> _userService;
        private readonly IMapper _mapper;
        private readonly ApiService _apiService;

        public UserController(ServiceLayer<UserDomainModel> userService, IMapper mapper, ApiService apiService)
        {
            _userService = userService;
            _mapper = mapper;
            _apiService = apiService;
        }

        // GET: UserController
        public async Task<IActionResult> Index()
        {
            List<UserDomainModel> users = await _userService.GetAllAsync(_apiService.GetApiUrl(ApiEndpoint.User));
            List<UserViewModel> usersViewModel = _mapper.Map<List<UserViewModel>>(users);
            return View(usersViewModel);
        }

        // GET: UserController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            UserDomainModel userDomainModel = await _userService.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.User), id);
            UserViewModel userViewModel = _mapper.Map<UserViewModel>(userDomainModel);
            return View(userViewModel);
        }

        // GET: UserController/Create
        public IActionResult Create()
        {
            return View(new UserViewModel());
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel userViewModel)
        {
            try
            {
                UserDomainModel userDomainModel = _mapper.Map<UserDomainModel>(userViewModel);
                int id = await _userService.AddAsync(_apiService.GetApiUrl(ApiEndpoint.User), userDomainModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Failed to create user.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error creating user: " + ex.Message);
            }
            return View(userViewModel);
        }

        // GET: UserController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            UserDomainModel userDomainModel = await _userService.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.User), id);
            UserViewModel userViewModel = _mapper.Map<UserViewModel>(userDomainModel);
            return View(userViewModel);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserViewModel userViewModel)
        {
            try
            {
                UserDomainModel userDomainModel = _mapper.Map<UserDomainModel>(userViewModel);
                bool isSuccess = await _userService.UpdateAsync($"{_apiService.GetApiUrl(ApiEndpoint.User)}/{id}", userDomainModel);
                if (isSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Failed to update user.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error updating user: " + ex.Message);
            }
            return View(userViewModel);
        }

        // GET: UserController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            UserDomainModel userDomainModel = await _userService.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.User), id);
            UserViewModel userViewModel = _mapper.Map<UserViewModel>(userDomainModel);
            return View(userViewModel);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                bool isSuccess = await _userService.DeleteAsync(_apiService.GetApiUrl(ApiEndpoint.User), id);
                if (isSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Failed to delete user.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error deleting user: " + ex.Message);
            }
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}
