using AutoMapper;
using BusinessLayer;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntity.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUsers _users;
        private readonly IMapper _mapper;
        public UserController(IUsers users,IMapper mapper) {
        
            _users = users;
            _mapper = mapper;
        }
        // GET: UserController
        public async Task<ActionResult> Index()
        {
            List<UserDomainModel> users = await _users.GetUsers();
            List<UserViewModel> usersViewModel = _mapper.Map<List<UserViewModel>>(users);
            return View(usersViewModel);
        }

        // GET: UserController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            UserDomainModel userDomainModel =await _users.GetUsersByid(id);
            UserViewModel userViewModel = _mapper.Map<UserViewModel>(userDomainModel);
            return View(userViewModel);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            UserViewModel userDomainModel = new UserViewModel();
            return View(userDomainModel);
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserDomainModel userDomainModel)
        {
            try
            {
                int id = await _users.insert(userDomainModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            UserDomainModel userDomainModel = await _users.GetUsersByid(id);
            UserViewModel userViewModel = _mapper.Map<UserViewModel>(userDomainModel);
            return View(userViewModel);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UserViewModel userViewModel)
        {
            try
            {
                UserDomainModel user = _mapper.Map<UserDomainModel>(userViewModel);
                bool b = await _users.update(user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            UserDomainModel userDomainModel = await _users.GetUsersByid(id);
            UserViewModel userViewModel = _mapper.Map<UserViewModel>(userDomainModel);
            return View(userViewModel);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                int b = await _users.delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
