using Microsoft.AspNetCore.Mvc;


namespace UrlsAndRoutes.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() => View();
    }
}
