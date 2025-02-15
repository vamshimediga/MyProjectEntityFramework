using AutoMapper;
using BusinessLayer;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LawyerController : Controller
    {
        private readonly ILawyer _lawyer;
        private readonly IMapper _mapper;
        public LawyerController(ILawyer lawyer, IMapper mapper)
        {
            _lawyer = lawyer;
            _mapper = mapper;
        }

        // GET: LawyerController
        public async Task<ActionResult> Index()
        {
            IEnumerable<Lawyer> lawyers =  await _lawyer.GetAllLawyers();
            IEnumerable<LawyerViewModel> lawyerViewModels = _mapper.Map<IEnumerable<LawyerViewModel>>(lawyers);
             return View(lawyerViewModels);
        }


        // GET: LawyerController/Create
        public ActionResult Create()
        {
            LawyerViewModel lawyyer = new LawyerViewModel();
            return View(lawyyer);
        }

        // POST: LawyerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LawyerViewModel lawyyer)
        {
            try
            {
                Lawyer lawyer = _mapper.Map<Lawyer>(lawyyer);
                await _lawyer.AddLawyer(lawyer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LawyerController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Lawyer lawyer = await _lawyer.GetLawyerById(id);
            LawyerViewModel lawyerViewModels = _mapper.Map<LawyerViewModel>(lawyer);
            return View(lawyerViewModels);
        }

        // POST: LawyerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, LawyerViewModel lawyerViewModels)
        {
            try
            {
                Lawyer lawyer = _mapper.Map<Lawyer>(lawyerViewModels);
                await _lawyer.UpdateLawyer(lawyer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LawyerController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Lawyer lawyer = await _lawyer.GetLawyerById(id);
            LawyerViewModel lawyerViewModels = _mapper.Map<LawyerViewModel>(lawyer);
            return View(lawyerViewModels);
        }

        // POST: LawyerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _lawyer.DeleteLawyer(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
