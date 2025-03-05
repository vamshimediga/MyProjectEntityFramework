using AutoMapper;
using BusinessLayer;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ResidentController : Controller
    {
        private readonly IResident _resident;
        private readonly IMapper _mapper;
        public ResidentController(IResident resident,IMapper mapper) {
        
            _mapper = mapper;
            _resident = resident;
        }
        // GET: ResidentController
        public async Task<ActionResult> Index()
        {
            List<Resident> residents = await _resident.GetResidentsAsync();
            List<ResidentViewModel> viewModel = _mapper.Map<List<ResidentViewModel>>(residents);  
            return View(viewModel);
        }

        // GET: ResidentController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Resident resident =  await _resident.GetResidentByIdAsync(id);
            ResidentViewModel residentViewModel = _mapper.Map<ResidentViewModel>(resident);
            return View(residentViewModel);
        }

        // GET: ResidentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResidentController/Create
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

        // GET: ResidentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ResidentController/Edit/5
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

        // GET: ResidentController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Resident resident = await _resident.GetResidentByIdAsync(id);
            ResidentViewModel residentViewModel = _mapper.Map<ResidentViewModel>(resident);
            return View(residentViewModel);
        }

        // POST: ResidentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _resident.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
