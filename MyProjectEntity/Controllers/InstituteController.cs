using AutoMapper;
using BusinessLayer;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntity.Controllers
{
    public class InstituteController : Controller
    {
        private readonly IInstitute _institute;
        private readonly IMapper _mapper;
        public InstituteController(IInstitute institute,IMapper mapper) { 
        _institute = institute;
            _mapper = mapper;
        }
        // GET: InstituteController
        public async Task<ActionResult> Index()
        {
            List<Institute> institutes = await _institute.GetInstitutes(); 
            List<InstituteViewModel> viewModel = _mapper.Map<List<InstituteViewModel>>(institutes);
            return View(viewModel);
        }

        // GET: InstituteController/Details/5
        public async Task<ActionResult> Details(int id, bool popUp = false)
        {
            Institute institute = await _institute.GetInstituteByid(id);
            InstituteViewModel instituteViewModel = _mapper.Map<InstituteViewModel>(institute);

            if (popUp)
            {
                // If popUp is true, return the PartialView to be loaded into the modal
                return PartialView("_InstituteDetails", instituteViewModel);
            }

            return View(instituteViewModel);
        }


        // GET: InstituteController/Create
        public ActionResult Create()
        {
            InstituteViewModel instituteViewModel = new InstituteViewModel();
            return PartialView("_Create", instituteViewModel);
        }

        // POST: InstituteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(InstituteViewModel instituteViewModel)
        {
            try
            {
                Institute institute = _mapper.Map<Institute>(instituteViewModel);
                int id =await  _institute.insert(institute);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InstituteController/Edit/5
        public async Task<ActionResult> Edit(int id, bool popUp = false)
        {
            Institute institute = await _institute.GetInstituteByid(id);
            InstituteViewModel instituteViewModel = _mapper.Map<InstituteViewModel>(institute);

            if (popUp)
            {
                // If popUp is true, return the PartialView to be loaded into the modal
                return PartialView("_Edit", instituteViewModel);
            }

            return View(instituteViewModel);
        }

        // POST: InstituteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(InstituteViewModel instituteViewModel)
        {
            try
            {
                Institute institute = _mapper.Map<Institute>(instituteViewModel);
                int id = await _institute.update(institute);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InstituteController/Delete/5
        public async Task<ActionResult> Delete(int id, bool popUp = false)
        {
            Institute institute = await _institute.GetInstituteByid(id);
            InstituteViewModel instituteViewModel = _mapper.Map<InstituteViewModel>(institute);
            return PartialView("_Delete", instituteViewModel);
        }

        // POST: InstituteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                int Id = await _institute.delete(id); 
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
