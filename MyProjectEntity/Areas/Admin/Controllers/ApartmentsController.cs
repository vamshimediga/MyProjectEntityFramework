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
    public class ApartmentsController : Controller
    {
        private readonly IApartment _apartments;
        private readonly IMapper _mapper;
        private readonly IResident _resident;
      public  ApartmentsController(IApartment apartment, IMapper mapper,IResident resident)
        {

            _apartments = apartment;
            _mapper = mapper;  
            _resident = resident;

        }
        [HttpGet]
        [Route("Admin/ApartmentsList")] // Explicitly define full route
        // GET: ApartmentsController
        public async Task<ActionResult> Index()
        {
           IEnumerable<Apartment> apartments =  await _apartments.GetAllApartmentsAsync();
            IEnumerable<ApartmentViewModel> apartmentViewModels = _mapper.Map<IEnumerable<ApartmentViewModel>>(apartments);
            return View(apartmentViewModels);
        }

        // GET: ApartmentsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Apartment apartment = await _apartments.GetApartmentByIdAsync(id);
            ApartmentViewModel apartmentViewModel =  _mapper.Map<ApartmentViewModel>(apartment);    
            return View(apartmentViewModel);
        }

        // GET: ApartmentsController/Create
        public async Task<ActionResult> Create()
        {
            ApartmentViewModel apartmentViewModel = new ApartmentViewModel();
            List<Resident> residents = new List<Resident>();
            residents = await _resident.GetResidentsAsync();
            List<ResidentViewModel> residentViewModels = _mapper.Map<List<ResidentViewModel>>(residents);
            apartmentViewModel.Residents = residentViewModels;
            return View(apartmentViewModel);
        }

        // POST: ApartmentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ApartmentViewModel apartmentViewModel)
        {
            try
            {
                if (!ModelState.IsValid) // Ensure model validation
                {
                    List<Resident> residents = new List<Resident>();
                    residents = await _resident.GetResidentsAsync();
                    List<ResidentViewModel> residentViewModels = _mapper.Map<List<ResidentViewModel>>(residents);
                    apartmentViewModel.Residents = residentViewModels;
                    return View(apartmentViewModel); // Return the view with validation errors
                }
                else
                {

                    Apartment apartment = _mapper.Map<Apartment>(apartmentViewModel);
                    await _apartments.InsertApartmentAsync(apartment);
                    TempData["SuccessMessage"] = "Apartment created successfully!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while saving the apartment.");
                return View(apartmentViewModel);
            }
        }

        // GET: ApartmentsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Apartment apartment = await _apartments.GetApartmentByIdAsync(id);
            List<Resident> residents = new List<Resident>();
            residents = await _resident.GetResidentsAsync();
            ICollection<ResidentViewModel> residentViewModels = _mapper.Map<List<ResidentViewModel>>(residents);
            ApartmentViewModel apartmentViewModel = _mapper.Map<ApartmentViewModel>(apartment);
            apartmentViewModel.Residents = residentViewModels;
            return View(apartmentViewModel);
        }

        // POST: ApartmentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ApartmentViewModel apartmentViewModel)
        {
            try
            {
                Apartment apartment = _mapper.Map<Apartment>(apartmentViewModel);
                await _apartments.UpdateApartmentAsync(apartment);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ApartmentsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Apartment apartment = await _apartments.GetApartmentByIdAsync(id);
            ApartmentViewModel apartmentViewModel = _mapper.Map<ApartmentViewModel>(apartment);
            return View(apartmentViewModel);
        }

        // POST: ApartmentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await  _apartments.DeleteApartmentAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
