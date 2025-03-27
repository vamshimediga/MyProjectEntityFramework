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
    public class OpportunityController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IOpportunity _opportunity;
        public OpportunityController(IOpportunity opportunity,IMapper mapper) {
        
            _mapper = mapper;
            _opportunity = opportunity;
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: OpportunityController
        public async Task<JsonResult> GetOpportunities()
        {
            List<Opportunity> opportunities = await _opportunity.GetAllOpportunitiesAsync();
            List<OpportunityViewModel> opportunityViewModels = _mapper.Map<List<OpportunityViewModel>>(opportunities);
            return Json(opportunityViewModels);
        }

        // GET: OpportunityController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Opportunity opportunity = await _opportunity.GetOpportunityByIdAsync(id);
            OpportunityViewModel opportunityViewModel = _mapper.Map<OpportunityViewModel>(opportunity);
            return View(opportunityViewModel);
        }

        // GET: OpportunityController/Create
        public ActionResult Create()
        {
            OpportunityViewModel opportunityViewModel = new OpportunityViewModel();
            return View(opportunityViewModel);
        }

        // POST: OpportunityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OpportunityViewModel opportunityViewModel)
        {
            try
            {
                Opportunity opportunity = _mapper.Map<Opportunity>(opportunityViewModel);
                int id = await _opportunity.InsertOpportunityAsync(opportunity);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OpportunityController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Opportunity opportunity = await _opportunity.GetOpportunityByIdAsync(id);
            OpportunityViewModel opportunityViewModel = _mapper.Map<OpportunityViewModel>(opportunity);
            return View(opportunityViewModel);
        }

        // POST: OpportunityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, OpportunityViewModel opportunityViewModel)
        {
            try
            {
                Opportunity opportunity = _mapper.Map<Opportunity>(opportunityViewModel);
                bool b = await _opportunity.UpdateOpportunityAsync(opportunity);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OpportunityController/Delete/5
        //public async Task<ActionResult> Delete(int id)
        //{
        //    Opportunity opportunity = await _opportunity.GetOpportunityByIdAsync(id);
        //    OpportunityViewModel opportunityViewModel = _mapper.Map<OpportunityViewModel>(opportunity);
        //    return View(opportunityViewModel);
        //}

        // POST: OpportunityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                bool isDeleted = await _opportunity.DeleteOpportunityAsync(id);
                if (isDeleted)
                {
                    return Json(new { success = true, message = "Opportunity deleted successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to delete opportunity." });
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
