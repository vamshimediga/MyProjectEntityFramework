using AutoMapper;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class OpptypeController : Controller
    {
        private readonly ServiceLayer<OpptypeDomainModel> _service;
        private readonly IMapper _mapper;
        private readonly ApiService _apiService;

        public OpptypeController(ServiceLayer<OpptypeDomainModel> service, IMapper mapper, ApiService apiService)
        {
            _service = service;
            _mapper = mapper;
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var opptypesDto = await _service.GetAllAsync(_apiService.GetApiUrl(ApiEndpoint.Opptype));
            var viewModel = _mapper.Map<List<OpptypeViewModel>>(opptypesDto);
            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var opptypeDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Opptype), id);
            var viewModel = _mapper.Map<OpptypeViewModel>(opptypeDto);
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OpptypeViewModel viewModel)
        {
            try
            {
                var opptype = _mapper.Map<OpptypeDomainModel>(viewModel);
                int id = await _service.AddAsync(_apiService.GetApiUrl(ApiEndpoint.Opptype), opptype);
                if (id > 0)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error creating opptype: " + ex.Message);
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var opptypeDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Opptype), id);
            var viewModel = _mapper.Map<OpptypeViewModel>(opptypeDto);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OpptypeViewModel viewModel)
        {
            try
            {
                var opptype = _mapper.Map<OpptypeDomainModel>(viewModel);
                bool isSuccess = await _service.UpdateAsync($"{_apiService.GetApiUrl(ApiEndpoint.Opptype)}/{id}", opptype);
                if (isSuccess)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error updating opptype: " + ex.Message);
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var opptypeDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Opptype), id);
            var viewModel = _mapper.Map<OpptypeViewModel>(opptypeDto);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int OpptypeID)
        {
            bool isSuccess = await _service.DeleteAsync(_apiService.GetApiUrl(ApiEndpoint.Opptype), OpptypeID);
            if (isSuccess)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error deleting opptype.");
            return RedirectToAction(nameof(Delete), new { id = OpptypeID });
        }

        [HttpGet]
        public IActionResult UserCreate()
        {
            UserRegistration userRegistration = new UserRegistration();
            return View(userRegistration);
        }
        [HttpPost]
        public IActionResult UserCreate(UserRegistration userRegistration) {

            if (ModelState.IsValid)
            {
                // Save to database or perform desired actions here

                return RedirectToAction("Index"); // Redirect after success
            }

            return View(userRegistration);

        }
    }
}
