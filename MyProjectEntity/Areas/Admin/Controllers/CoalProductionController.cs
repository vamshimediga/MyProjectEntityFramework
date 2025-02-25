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
    public class CoalProductionController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICoalProduction _coalProduction;
        public CoalProductionController(IMapper mapper,ICoalProduction coalProduction) {
        
        _mapper = mapper;
        _coalProduction = coalProduction;

        }
        // GET: CoalProductionController
        public async Task<ActionResult> Index()
        {
            IEnumerable<CoalProduction> coalProductions = await _coalProduction.GetCoalProductions();
            IEnumerable<CoalProductionViewModel> coalProductionViewModels = _mapper.Map<IEnumerable<CoalProductionViewModel>>(coalProductions);
            return View(coalProductionViewModels);
        }

        // GET: CoalProductionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CoalProductionController/Create
        public ActionResult Create()
        {
            CoalProductionViewModel coalProductionViewModel = new CoalProductionViewModel();
            return View(coalProductionViewModel);
        }

        // POST: CoalProductionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CoalProductionViewModel coalProductionViewModel)
        {
            try
            {
                CoalProduction coalProduction = _mapper.Map<CoalProduction>(coalProductionViewModel);
                await _coalProduction.Insert(coalProduction); 
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CoalProductionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CoalProductionController/Edit/5
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

        // GET: CoalProductionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CoalProductionController/Delete/5
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
