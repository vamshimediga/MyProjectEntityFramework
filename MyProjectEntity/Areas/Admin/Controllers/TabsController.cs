using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TabsController : Controller
    {
        // GET: TabsController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadTab1()
        {
            List<PersonTabViewModel> people = new List<PersonTabViewModel>()
            {
            new PersonTabViewModel { Id = 1, Name = "John Doe", Age = 30 },
            new PersonTabViewModel { Id = 2, Name = "Jane Smith", Age = 25 },
            new PersonTabViewModel { Id = 3, Name = "Mike Johnson", Age = 35 }
            };
            return PartialView("_Tab1", people);
        }

        public ActionResult LoadTab2()
        {
            List<ProductTabViewModel> products = new List<ProductTabViewModel>(){
            new ProductTabViewModel { Id = 1, ProductName = "Laptop", Price = 1200 },
            new ProductTabViewModel { Id = 2, ProductName = "Phone", Price = 800 },
            new ProductTabViewModel { Id = 3, ProductName = "Tablet", Price = 600 }
            };
            return PartialView("_Tab2", products);
        }

        public ActionResult LoadTab3()
        {
            List<OrderTabViewModel> orders = new List<OrderTabViewModel>(){
            new OrderTabViewModel { Id = 1, CustomerName = "John Doe", OrderDate = DateTime.Parse("2024-03-10") },
            new OrderTabViewModel { Id = 2, CustomerName = "Jane Smith", OrderDate = DateTime.Parse("2024-03-15") },
            new OrderTabViewModel { Id = 3, CustomerName = "Mike Johnson", OrderDate =DateTime.Parse("2024-03-20") }
             };
            return PartialView("_Tab3", orders);
        }


    }
}
