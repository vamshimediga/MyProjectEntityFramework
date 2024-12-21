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
        public async Task<ActionResult> Details(int id)
        {
            Customer customer = await _customerRepository.GetCustomerByIdAsync(id);
            // Map to ViewModel
            CustomerViewModel viewModels = _mapper.Map<CustomerViewModel>(customer);
            return View(viewModels);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            CustomerViewModel customerView = new CustomerViewModel();
            return View(customerView);
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel customerView)
        {
            try
            {
                Customer customer =_mapper.Map<Customer>(customerView);
                _customerRepository.AddCustomerAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Customer customer = await _customerRepository.GetCustomerByIdAsync(id);
            // Map to ViewModel
            CustomerViewModel viewModels = _mapper.Map<CustomerViewModel>(customer);
            return View(viewModels);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CustomerViewModel viewModels)
        {
            try
            {
                Customer customer = _mapper.Map<Customer>(viewModels);
                await _customerRepository.UpdateCustomerAsync(customer);
                return Json(new { success = true, message = "Customer updated successfully." });
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Customer customer = await _customerRepository.GetCustomerByIdAsync(id);
            // Map to ViewModel
            CustomerViewModel viewModels = _mapper.Map<CustomerViewModel>(customer);
            return View(viewModels);
           
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _customerRepository.DeleteCustomerAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
