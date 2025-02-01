using AutoMapper;
using BusinessLayer;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PassportController : Controller
    {
        // GET: LeadController
        private readonly IPersons _person;
        private readonly IMapper _mapper;
        private readonly IPassport _passport;
        public PassportController(IPersons person, IMapper mapper, IPassport passport)
        {
            _person = person;
            _mapper = mapper;
            _passport = passport;
        }
        // GET: PassportController
        public async Task<ActionResult> Index()
        {
            List<Passport> passports = await _passport.GetPassports();
            List<PassportViewModel> viewModel = _mapper.Map<List<PassportViewModel>>(passports);
            return View(viewModel);
        }

        // GET: PassportController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PassportController/Create
        public async Task<ActionResult> Create()
        {
            PassportViewModel passportViewModel = new PassportViewModel();

            return View(passportViewModel);
        }

        // POST: PassportController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PassportController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PassportController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PassportController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PassportController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
