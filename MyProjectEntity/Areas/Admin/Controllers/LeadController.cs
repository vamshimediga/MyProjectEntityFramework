using AutoMapper;
using BusinessLayer;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LeadController : Controller
    {
        // GET: LeadController
        private readonly ILead _lead;
        private readonly IMapper _mapper;
        public LeadController(ILead lead,IMapper mapper)
        {
            _lead = lead;
            _mapper = mapper;
        }
        public async Task<ActionResult> Index()
        {
            List<Lead> leads = await _lead.leads();
            List<LeadViewModel> leadsViewModel = _mapper.Map<List<LeadViewModel>>(leads);
            return View(leadsViewModel);
        }

        // GET: LeadController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Lead lead = await _lead.Lead(id);
            LeadViewModel leadViewModel = _mapper.Map<LeadViewModel>(lead); 
            return PartialView("_Details", leadViewModel);
        }

        // GET: LeadController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeadController/Create
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

        // GET: LeadController/Edit/5
    
        public async Task<ActionResult> Edit(int id)
        {
            Lead lead = await _lead.Lead(id);
            LeadViewModel leadViewModel = _mapper.Map<LeadViewModel>(lead);
            return PartialView("_Edit", leadViewModel);
        }
        // POST: LeadController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LeadViewModel leadViewModel)
        {
           
                    Lead lead = _mapper.Map<Lead>(leadViewModel);
                    bool isSuccess = await _lead.Update(lead);
           // return Json(new { success = true });
            return RedirectToAction(nameof(Index));
        }




        // GET: LeadController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeadController/Delete/5
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
