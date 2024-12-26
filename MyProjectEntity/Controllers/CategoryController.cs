using AutoMapper;
using BusinessLayer;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntity.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository,IMapper mapper) { 
        _categoryRepository = categoryRepository;
        _mapper=mapper;
        }
        // GET: CategoryController
        public async Task<ActionResult> Index()
        {
            IEnumerable<Category> categories  = await _categoryRepository.GetAllCategoriesAsync();
            IEnumerable<CategoryViewModel> categoriesViewModel = _mapper.Map<IEnumerable<CategoryViewModel>>(categories);

            // Return the view with the mapped data
            return View(categoriesViewModel);
        }

        // GET: CategoryController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Category category = await _categoryRepository.GetCategoryByIdAsync(id);
            CategoryViewModel categoryViewModel = _mapper.Map<CategoryViewModel>(category); 
            return View(categoryViewModel);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel();
            return View(categoryViewModel);
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel categoryViewModel)
        {
            try
            {
                Category category = _mapper.Map<Category>(categoryViewModel);
                _categoryRepository.InsertCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Category category = await _categoryRepository.GetCategoryByIdAsync(id);
            CategoryViewModel categoryViewModel = _mapper.Map<CategoryViewModel>(category);
            return View(categoryViewModel);
            
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel categoryViewModel)
        {
            try
            {
                Category category = _mapper.Map<Category>(categoryViewModel);
                _categoryRepository.UpdateCategoryAsync(category);
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Category category = await _categoryRepository.GetCategoryByIdAsync(id);
            CategoryViewModel categoryViewModel = _mapper.Map<CategoryViewModel>(category);
            return View(categoryViewModel);
          
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                   await _categoryRepository.DeleteCategoryAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
