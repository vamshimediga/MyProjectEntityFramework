using AutoMapper;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class CompanyTypeController : Controller
    {
        private readonly ServiceLayer<CompanyTypeDomainModel> _service;
        private readonly IMapper _mapper;
        private readonly ApiService _apiService;

        public CompanyTypeController(ServiceLayer<CompanyTypeDomainModel> service, IMapper mapper, ApiService apiService)
        {
            _service = service;
            _mapper = mapper;
            _apiService = apiService;
        }

        // GET: CompanyType
        public async Task<IActionResult> Index()
        {
            List<CompanyTypeDomainModel> companyTypesDto = await _service.GetAllAsync(_apiService.GetApiUrl(ApiEndpoint.CompanyType));
            List<CompanyTypeViewModel> viewModel = _mapper.Map<List<CompanyTypeViewModel>>(companyTypesDto);
            return View(viewModel);
        }

        // GET: CompanyType/Details/5
      

        // GET: CompanyType/Create
        public IActionResult Create()
        {
            CompanyTypeViewModel companyTypeViewModel = new CompanyTypeViewModel();
            return View(companyTypeViewModel);
        }

        // POST: CompanyType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyTypeViewModel viewModel)
        {
            try
            {
                CompanyTypeDomainModel entity = _mapper.Map<CompanyTypeDomainModel>(viewModel);
                int id = await _service.AddAsync(_apiService.GetApiUrl(ApiEndpoint.CompanyType), entity);

                if (id != 0)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error creating company type: " + ex.Message);
            }
            return View(viewModel);
        }

        // GET: CompanyType/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            CompanyTypeDomainModel dto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.CompanyType), id);
            CompanyTypeViewModel viewModel = _mapper.Map<CompanyTypeViewModel>(dto);
            return View(viewModel);
        }

        // POST: CompanyType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CompanyTypeViewModel viewModel)
        {
            try
            {
                CompanyTypeDomainModel entity = _mapper.Map<CompanyTypeDomainModel>(viewModel);
                bool isSuccess = await _service.UpdateAsync($"{_apiService.GetApiUrl(ApiEndpoint.CompanyType)}/{id}", entity);

                if (isSuccess)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error updating company type: " + ex.Message);
            }
            return View(viewModel);
        }

        // GET: CompanyType/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            CompanyTypeDomainModel dto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.CompanyType), id);
            CompanyTypeViewModel viewModel = _mapper.Map<CompanyTypeViewModel>(dto);
            return View(viewModel);
        }

        // POST: CompanyType/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int CompanyTypeID)
        {
            bool isSuccess = await _service.DeleteAsync(_apiService.GetApiUrl(ApiEndpoint.CompanyType), CompanyTypeID);
            if (isSuccess)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error deleting company type.");
            return RedirectToAction(nameof(Delete), new { id = CompanyTypeID });
        }
    }
}
