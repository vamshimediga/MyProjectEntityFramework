using AutoMapper;
using BusinessLayer;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MyProjectEntity.Controllers
{
    public class EmployeeController : Controller
    {
        public readonly IEmployees _emp;
        public readonly IMapper _mapper;
        public EmployeeController(IEmployees emp, IMapper mapper)
        {
            _emp = emp;
            _mapper = mapper;
        }
        // GET: EmployeeController
        public async Task<ActionResult> Index()
        {
            List<Employee> employee = await  _emp.GetEmployeesAsync();
            List<EmployeeViewModel> employeeViewModel = _mapper.Map<List<EmployeeViewModel>>(employee);
            return View(employeeViewModel);
        }

        // GET: EmployeeController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Employee employee = await _emp.GetEmployeeByIdAsync(id);
            EmployeeViewModel employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);
            return View(employeeViewModel);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            return View(employeeViewModel);
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeViewModel employeeViewModel)
        {
            try
            {
                Employee employee = _mapper.Map<Employee>(employeeViewModel);
                bool issuccess = await _emp.Insert(employee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Employee employee = await _emp.GetEmployeeByIdAsync(id);
            EmployeeViewModel employeeView = _mapper.Map<EmployeeViewModel>(employee);
            return View(employeeView);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EmployeeViewModel employeeViewModel)
        {
            try
            {
                Employee employee = _mapper.Map<Employee>(employeeViewModel);
                bool issuccess = await _emp.Update(employee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Employee employee = await _emp.GetEmployeeByIdAsync(id);
            EmployeeViewModel employeeView = _mapper.Map<EmployeeViewModel>(employee);
            return View(employeeView);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                bool issuuess = await _emp.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
