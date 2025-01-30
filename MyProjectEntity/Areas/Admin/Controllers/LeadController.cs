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
            LeadViewModel leadViewModel = new LeadViewModel();
            return PartialView("_Create", leadViewModel);
        }

        // POST: LeadController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LeadViewModel leadViewModel)
        {
            try
            {
                
                Lead lead = _mapper.Map<Lead>(leadViewModel);
                bool isSuccess = await _lead.Insert(lead);
                // return Json(new { success = true });
               //return RedirectToAction(nameof(Index));
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




        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Lead lead = await _lead.Lead(id);
                LeadViewModel leadViewModel = _mapper.Map<LeadViewModel>(lead);
                return PartialView("_Delete", leadViewModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while fetching lead details for deletion: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: LeadController/Delete/5
        [HttpGet]
     
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                bool isSuccess = await _lead.Delete(id);

                if (isSuccess)
                {
                    return Json(new { success = true, message = "Lead deleted successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to delete the lead." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"An error occurred while deleting the lead: {ex.Message}" });
            }
        }


    }
}
