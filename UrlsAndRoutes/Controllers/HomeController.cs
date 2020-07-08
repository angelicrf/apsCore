using Microsoft.AspNetCore.Mvc;
using UrlsAndRoutes.Infrastructure;
using UrlsAndRoutes.Models;

namespace UrlsAndRoutes.Controllers
{
    //[AdditionalActions]
    public class HomeController : Controller
    {
        public IActionResult Index() => View("Result", new Result
        {
            Controller = nameof(HomeController),
            Action = nameof(Index)
        });
        [ActionName("Index")]
        //[ActionName("Details")]
        [HttpPost]

        public IActionResult Other() => View("Result", new Result
        {
            Controller = nameof(HomeController),
            Action = nameof(Other)
        });
        //[ActionNamePrefix("Do")]
        //[ActionName("Details")]
        //[AddAction("Details")]
         [UserAgent("Edge")]
        public IActionResult List() => View("Result", new Result
        {
            Controller = nameof(HomeController),
            Action = nameof(List)
        });
    }
}
