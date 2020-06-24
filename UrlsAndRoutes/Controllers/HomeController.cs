using Microsoft.AspNetCore.Mvc;
using UrlsAndRoutes.Models;

namespace UrlsAndRoutes.Controllers
{
    public class HomeController : Controller
    {
        //public ViewResult Index() => View(new MemoryRepository().Products);
        public IRepository Repository { get; set; } = new MemoryRepository();
        public ViewResult Index() => View(Repository.Products);
    }
}
