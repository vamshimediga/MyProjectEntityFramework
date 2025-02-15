using AutoMapper;
using BusinessLayer;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntity.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudent _student;
        private readonly IMapper _mapper;
        public StudentController(IStudent student,IMapper mapper)
        {
            _student = student;
            _mapper = mapper;
        }
        // GET: StudentController1cs
        public async Task<ActionResult> Index()
        {
            List<Student> students = await _student.allStudents();
            List<StudentViewModel> studentViewModel = _mapper.Map<List<StudentViewModel>>(students);  
            return View(studentViewModel);
        }

        // GET: StudentController1cs/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Student student = await _student.students(id);
            StudentViewModel studentViewModel = _mapper.Map<StudentViewModel>(student); 
            return View(studentViewModel);
        }

        // GET: StudentController1cs/Create
        public  ActionResult Create()
        {
            StudentViewModel viewModel = new StudentViewModel();
            return View(viewModel);
        }

        // POST: StudentController1cs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StudentViewModel viewModel)
        {
            try
            {
                Student student = _mapper.Map<Student>(viewModel);
                int id = await _student.insert(student);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController1cs/Edit/5
        public  async Task<ActionResult> Edit(int id)
        {
            Student student = await _student.students(id);
            StudentViewModel studentViewModel = _mapper.Map<StudentViewModel>(student);
            return View(studentViewModel);
        }

        // POST: StudentController1cs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(StudentViewModel viewModel)
        {
            try
            {
                Student student = _mapper.Map<Student>(viewModel);
                int id = await _student.update(student);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController1cs/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Student student = await _student.students(id);
            StudentViewModel studentViewModel = _mapper.Map<StudentViewModel>(student);
            return View(studentViewModel);
           // return View();
        }

        // POST: StudentController1cs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
              int Id=  await _student.delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
