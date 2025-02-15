using AutoMapper;
using BusinessLayer;
using BusinessLayer.implemation;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntity.Controllers
{
    public class BookController : Controller
    {
        private readonly IBook _bookRepository;
        private readonly IMapper _mapper;
        public BookController(IBook bookRepository,IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;       
        }
        // GET: BookController
        public async Task<ActionResult> Index()
        {
            List<Book> books = (List<Book>)await _bookRepository.GetAllBooksAsync();
            List<BookViewModel> bookViewModel = _mapper.Map<List<BookViewModel>>(books);
            return View(bookViewModel);
        }

        // GET: BookController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Book book = await _bookRepository.GetBookByIdAsync(id);
            BookViewModel bookViewModel = _mapper.Map<BookViewModel>(book);
            return PartialView("_Details", bookViewModel);
        }


        // GET: BookController/Create
        public  ActionResult Create()
        {
            BookViewModel bookViewModel = new BookViewModel();
            return PartialView("_Create", bookViewModel);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BookViewModel bookViewModel)
        {
            try
            {
                Book book =_mapper.Map<Book>(bookViewModel);
                int id = await _bookRepository.InsertBookAsync(book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Book book = await _bookRepository.GetBookByIdAsync(id);
            BookViewModel bookViewModel = _mapper.Map<BookViewModel>(book);
            return PartialView("_Edit", bookViewModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BookViewModel bookViewModel)
        {
            try
            {
                Book book = _mapper.Map<Book>(bookViewModel);
                int id =await _bookRepository.UpdateBookAsync(book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return PartialView("_Edit");
            }
        }

        // GET: BookController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Book book = await _bookRepository.GetBookByIdAsync(id);
            BookViewModel bookViewModel = _mapper.Map<BookViewModel>(book);
            return PartialView("_Delete", bookViewModel);
          //  return View();
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _bookRepository.DeleteBookAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
