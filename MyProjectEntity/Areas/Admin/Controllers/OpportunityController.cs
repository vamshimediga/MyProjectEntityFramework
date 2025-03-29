using AutoMapper;
using BusinessLayer;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OpportunityController : Controller
    {
        private readonly ServiceLayer<Opportunity> _service;
        private readonly IMapper _mapper;
        private readonly ApiService _apiService;

        public OpportunityController(ServiceLayer<Opportunity> service, IMapper mapper, ApiService apiService)
        {
            _service = service;
            _mapper = mapper;
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            //List<Opportunity> opportunitiesDto = await _service.GetAllAsync(_apiService.GetApiUrl(ApiEndpoint.Opportunity));
            //List<OpportunityViewModel> viewModel = _mapper.Map<List<OpportunityViewModel>>(opportunitiesDto);
            //return View(viewModel);
            return View();
        }
        // GET: OpportunityController
        public async Task<JsonResult> GetOpportunities()
        {
            List<Opportunity> opportunitiesDto = await _service.GetAllAsync(_apiService.GetApiUrl(ApiEndpoint.Opportunity));
            List<OpportunityViewModel> viewModel = _mapper.Map<List<OpportunityViewModel>>(opportunitiesDto);
            return Json(viewModel);
        }
        public async Task<IActionResult> Details(int id)
        {
            Opportunity opportunityDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Opportunity), id);
            if (opportunityDto == null)
                return NotFound();

            OpportunityViewModel viewModel = _mapper.Map<OpportunityViewModel>(opportunityDto);
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View(new OpportunityViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OpportunityViewModel opportunityViewModel)
        {
            if (!ModelState.IsValid)
                return View(opportunityViewModel);

            try
            {
                var opportunity = _mapper.Map<Opportunity>(opportunityViewModel);
                int id = await _service.AddAsync(_apiService.GetApiUrl(ApiEndpoint.Opportunity), opportunity);

                if (id > 0)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error creating opportunity: " + ex.Message);
            }
            return View(opportunityViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Opportunity opportunityDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Opportunity), id);
            if (opportunityDto == null)
                return NotFound();

            OpportunityViewModel viewModel = _mapper.Map<OpportunityViewModel>(opportunityDto);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OpportunityViewModel opportunityViewModel)
        {
            if (!ModelState.IsValid)
                return View(opportunityViewModel);

            try
            {
                var opportunity = _mapper.Map<Opportunity>(opportunityViewModel);
                bool isSuccess = await _service.UpdateAsync($"{_apiService.GetApiUrl(ApiEndpoint.Opportunity)}/{id}", opportunity);

                if (isSuccess)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error updating opportunity: " + ex.Message);
            }
            return View(opportunityViewModel);
        }

        //public async Task<IActionResult> Delete(int id)
        //{
        //    Opportunity opportunityDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Opportunity), id);
        //    if (opportunityDto == null)
        //        return NotFound();

        //    OpportunityViewModel viewModel = _mapper.Map<OpportunityViewModel>(opportunityDto);
        //    return View(viewModel);
        //}

        
        [HttpPost]
        [ValidateAntiForgeryToken] // Ensures CSRF protection
        public async Task<JsonResult> Delete(int opportunityID)
        {
            try
            {
                // Simulating the delete operation from the service or repository
               bool isSuccess = await _service.DeleteAsync(_apiService.GetApiUrl(ApiEndpoint.Opportunity), opportunityID);

                if (!isSuccess)
                {
                    return Json(new { success = false, message = "Failed to delete the opportunity." });
                }

                return Json(new { success = true, message = "Opportunity deleted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

    }
}
