using Microsoft.AspNetCore.Mvc;
using UrlsAndRoutes.Models;
using Microsoft.Extensions.DependencyInjection;

namespace UrlsAndRoutes.Controllers
{
    public class HomeController : Controller
    {
        //public ViewResult Index() => View(new MemoryRepository().Products);
        //public IRepository Repository { get; set; } = new MemoryRepository();
        //public IRepository Repository { get; } = TypeBroker.Repository;
        private IRepository repository;
        private ProductTotalizer totalizer;
        public HomeController(IRepository repo, ProductTotalizer total)
        {
            repository = repo;
            totalizer = total;

        }
        public ViewResult Index([FromServices] ProductTotalizer totalizer)
        {
            
            IRepository repository = HttpContext.RequestServices.GetService<IRepository>();
            ViewBag.ShowRepo = repository.ToString();
            ViewBag.CalcTotal = totalizer.Repository.ToString();
            ViewBag.Total = totalizer.Total;

            return View(repository.Products);
        }
    }
}
