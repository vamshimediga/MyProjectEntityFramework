using AutoMapper;
using BusinessLayer;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntity.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderController(IOrderRepository orderRepository,IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;   
        }
        // GET: OrderController
        public async Task<ActionResult> Index()
        {
            List<Order> orders= (List<Order>)await _orderRepository.GetAllOrdersAsync();
            List<OrderViewModel> ordersViewModel= _mapper.Map<List<OrderViewModel>>(orders);
            return View(ordersViewModel);
        }

        // GET: OrderController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Order order= await _orderRepository.GetOrderByIdAsync(id);
            OrderViewModel orderViewModel = _mapper.Map<OrderViewModel>(order);
            return View(orderViewModel);
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            OrderViewModel orderViewModel = new OrderViewModel();
            return View(orderViewModel);
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderViewModel orderViewModel)
        {
            try
            {
                Order order = _mapper.Map<Order>(orderViewModel);
                _orderRepository.AddOrderAsync(order);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Order order = await _orderRepository.GetOrderByIdAsync(id);
            OrderViewModel orderViewModel = _mapper.Map<OrderViewModel>(order);
            return View(orderViewModel);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderViewModel orderViewModel)
        {
            try
            {
                Order order = _mapper.Map<Order>(orderViewModel);
                _orderRepository.UpdateOrderAsync(order);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Order order = await _orderRepository.GetOrderByIdAsync(id);
            OrderViewModel orderViewModel = _mapper.Map<OrderViewModel>(order);
            return View(orderViewModel);
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _orderRepository.DeleteOrderAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
