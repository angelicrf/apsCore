using Microsoft.AspNetCore.Mvc;
using UrlsAndRoutes.Infrastructure;
using UrlsAndRoutes.Models;

namespace UrlsAndRoutes.Controllers
{
    public class HomeController : Controller
    {
        //public ViewResult Index() => View(new MemoryRepository().Products);
        //public IRepository Repository { get; set; } = new MemoryRepository();
        //public IRepository Repository { get; } = TypeBroker.Repository;
        private IRepository repository;
        public HomeController(IRepository repo) => repository = repo;
        public ViewResult Index() => View(repository.Products);
    }
}
