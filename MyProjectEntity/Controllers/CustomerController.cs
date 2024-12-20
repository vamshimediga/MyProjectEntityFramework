using AutoMapper;
using BusinessLayer;
using Entities;
using EntitiesViewModel;
using Interfaces.@interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProjectEntity.Entities;
using Repository;

namespace MyProjectEntity.Controllers
{
    public class CustomerController : Controller
    {

        private readonly ICustomer _customerRepository;
        private readonly IMapper _mapper; // Inject AutoMapper

        public CustomerController(ICustomer customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        // GET: CustomerController
        public async Task<ActionResult> Index()
        {
            List<Customer> courses = (List<Customer>)await _customerRepository.GetAllCustomersAsync();

            // Map to ViewModel
            List<CustomerViewModel> viewModels = _mapper.Map<List<CustomerViewModel>>(courses);
            return View(viewModels);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
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

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerController/Edit/5
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

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
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
