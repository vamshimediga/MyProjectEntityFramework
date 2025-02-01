using AutoMapper;
using BusinessLayer;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PersonController : Controller
    {
        // GET: LeadController
        private readonly IPersons _person;
        private readonly IMapper _mapper;
        private readonly IPassport _passport;
        public PersonController(IPersons person, IMapper mapper, IPassport passport)
        {
            _person = person;
            _mapper = mapper;
            _passport = passport;
        }
        // GET: PersonController
        public async Task<ActionResult> Index()
        {
            List<Person> person = await _person.GetPeople();
            List<PersonViewModel> personViewModel = _mapper.Map<List<PersonViewModel>>(person);
            return View(personViewModel);
        }

        // GET: PersonController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PersonController/Create
        public async Task<ActionResult> Create()
        {
            PersonViewModel personViewModel = new PersonViewModel();

            List<Passport> passports = await _passport.GetPassports() ?? new List<Passport>();

            List<PassportViewModel> passportViewModels = _mapper.Map<List<PassportViewModel>>(passports);

            personViewModel.Passports = passportViewModels ?? new List<PassportViewModel>();

            return View(personViewModel);
        }

        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PersonViewModel personViewModel)
        {
            try
            {
                Person person =  _mapper.Map<Person>(personViewModel);
                int id = await _person.Insert(person);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Person persons = await _person.GetPerson(id);
            PersonViewModel personViewModel = _mapper.Map<PersonViewModel>(persons);
            return View(personViewModel);
        }

        // POST: PersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, PersonViewModel personViewModel)
        {
            try
            {
                Person person = _mapper.Map<Person>(personViewModel);
                int ids = await _person.Update(person);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PersonController/Delete/5
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
