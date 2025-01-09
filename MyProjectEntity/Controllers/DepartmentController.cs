using AutoMapper;
using BusinessLayer;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntity.Controllers
{
    public class DepartmentController : Controller
    {
        public readonly IDepartment _department;
        public readonly IMapper _mapper;
        public DepartmentController(IDepartment department,IMapper mapper) { 
        _department = department;
            _mapper = mapper;
        }   
        // GET: DepartmentController
        public async Task<ActionResult> Index()
        {
            List<Department> department = await _department.GetDepartments();
            List<DepartmentViewModel>  departmentViewModels = _mapper.Map<List<DepartmentViewModel>>(department); 
            return View(departmentViewModels);
        }

        // GET: DepartmentController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Department department = await _department.GetDepartmentById(id);
            DepartmentViewModel departmentViewModel = _mapper.Map<DepartmentViewModel>(department);
            return View(departmentViewModel);
        }

        // GET: DepartmentController/Create
        public ActionResult Create()
        {
            DepartmentViewModel departmentViewModel = new DepartmentViewModel();

            return View(departmentViewModel);
        }

        // POST: DepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DepartmentViewModel departmentViewModel)
        {
            try
            {
                Department department = _mapper.Map<Department>(departmentViewModel);
              int id =  await _department.Insert(department);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DepartmentController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Department department = await _department.GetDepartmentById(id);
            DepartmentViewModel departmentViewModel = _mapper.Map<DepartmentViewModel>(department);
            return View(departmentViewModel);
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DepartmentViewModel departmentViewModel)
        {
            try
            {
                Department department = _mapper.Map<Department>(departmentViewModel);
             bool id=   await _department.Update(department);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DepartmentController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Department department = await _department.GetDepartmentById(id);
            DepartmentViewModel departmentViewModel = _mapper.Map<DepartmentViewModel>(department);
            return View(departmentViewModel);
        }

        // POST: DepartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                bool isisuucess = await _department.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
