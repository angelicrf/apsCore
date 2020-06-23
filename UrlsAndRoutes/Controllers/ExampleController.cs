using System;
using Microsoft.AspNetCore.Mvc;

namespace UrlsAndRoutes.Controllers
{
    public class ExampleController : Controller
    {
        // public ViewResult Index() => View(DateTime.Now);
        public ViewResult Index()
        {
            ViewBag.Message = "Hello";
            ViewBag.Date = DateTime.Now;
            return View();
        }
        // public JsonResult Index() => Json(new[] { "Alice", "Bob", "Joe" });
        // public ObjectResult Index() => Ok(new string[] { "Alice", "Brian", "Joe" });
        // public ContentResult Index() => Content("[\"Alice\",\"Bob\",\"Joe\"]", "application/json");
        // public ViewResult Result() => View((object)"Rendering from Example Controller using Result.cshtml");
        // public RedirectResult Redirect() => Redirect("/Example/Index");
        // public StatusCodeResult Index() => NotFound();
        public RedirectToRouteResult Redirect() =>
                         RedirectToRoute(new
                         {
                             controller = "Example",
                             action = "Index",
                             ID = "MyID"
                         });
        public RedirectToActionResult Redirect2() => RedirectToAction(nameof(ExampleController.Index));
    }
}
