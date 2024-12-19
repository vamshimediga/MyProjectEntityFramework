using AutoMapper;
using EntitiesViewModel;
using Interfaces.@interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProjectEntity.Entities;

namespace MyProjectEntity.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper; // Inject AutoMapper
       
        public CoursesController(ICourseRepository courseRepository,IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<Course> courses = (List<Course>)await _courseRepository.GetAllCoursesAsync();
            List<CourseViewModel> viewModels = _mapper.Map<List<CourseViewModel>>(courses);
            return View(viewModels);
        }

        // GET: CoursesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Course course = await _courseRepository.GetCourseByIdAsync(id);
            CourseViewModel viewModel = _mapper.Map<CourseViewModel>(course);
            return View(viewModel);
        }

        // GET: CoursesController/Create
        public  ActionResult Create()
        {
            CourseViewModel model = new CourseViewModel();
            return View(model);
        }

        // POST: CoursesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                Course course = _mapper.Map<Course>(model);
                await _courseRepository.AddCourseAsync(course);
                return Json(new { success = true });
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errorMessage = string.Join("<br/>", errors) });
            }
        }


        // GET: CoursesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CoursesController/Edit/5
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

        // GET: CoursesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CoursesController/Delete/5
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
