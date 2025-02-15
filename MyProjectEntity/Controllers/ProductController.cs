using AutoMapper;
using BusinessLayer;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntity.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            List<Product> products = await _productRepository.GetAllProductsAsync();
            List<ProductViewModel> viewModels = _mapper.Map<List<ProductViewModel>>(products);
            return View(viewModels);
        }

        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Product product = await _productRepository.GetProductByIdAsync(id);
            ProductViewModel productViewModel = _mapper.Map<ProductViewModel>(product); 
            return View(productViewModel);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            ProductViewModel product = new ProductViewModel();
            
            return View(product);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel product)
        {
            try
            {
                Product productdata=_mapper.Map<Product>(product);
                _productRepository.AddProductAsync(productdata);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Product product = await _productRepository.GetProductByIdAsync(id);
            ProductViewModel productViewModel = _mapper.Map<ProductViewModel>(product);
            return View(productViewModel);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel product)
        {
            try
            {
               Product product1= _mapper.Map<Product>(product);
                _productRepository.UpdateProductAsync(product1);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Product product = await _productRepository.GetProductByIdAsync(id);
            ProductViewModel productViewModel = _mapper.Map<ProductViewModel>(product);
            return View(productViewModel);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _productRepository.DeleteProductAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
