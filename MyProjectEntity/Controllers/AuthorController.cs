using AutoMapper;
using BusinessLayer;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntity.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthors _authorRepository;
        private readonly IMapper _mapper;
        public AuthorController(IAuthors authors,IMapper mapper) { 
            _authorRepository = authors;
            _mapper = mapper;   
        }
        // GET: AuthorController
        public async Task<ActionResult> Index()
        {
            List<Author> authors = await _authorRepository.GetAuthors();
            List<AuthorViewModel> viewModels = _mapper.Map<List<AuthorViewModel>>(authors);
             return View(viewModels);
        }

        // GET: AuthorController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Author author = await _authorRepository.GetById(id);
            AuthorViewModel authorViewModel = _mapper.Map<AuthorViewModel>(author); 
            return PartialView("_Details", authorViewModel);
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {
            AuthorViewModel authorViewModel = new AuthorViewModel();
            return PartialView("_Create", authorViewModel);
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AuthorViewModel authorViewModel)
        {
            try
            {
                Author author = _mapper.Map<Author>(authorViewModel);
                await _authorRepository.Insert(author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Author author = await _authorRepository.GetById(id);
            AuthorViewModel authorViewModel = _mapper.Map<AuthorViewModel>(author);
            return PartialView("_Edit", authorViewModel);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AuthorViewModel authorViewModel)
        {
            try
            {
                Author author = _mapper.Map<Author>(authorViewModel);
                await _authorRepository.Update(author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Author author = await _authorRepository.GetById(id);
            AuthorViewModel authorViewModel = _mapper.Map<AuthorViewModel>(author);
            return PartialView("_Delete", authorViewModel);
           
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _authorRepository.Delete(id);
                 return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
