using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlsAndRoutes.Infrastructure;

namespace UrlsAndRoutes.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("SimpleForm");
        }
        //public ViewResult ReceiveForm()
        //{
        //    var name = Request.Form["name"];
        //    var city = Request.Form["city"];
        //    return View("Result", $"{name} lives in {city}");
        //}
        //public ViewResult ReceiveForm(string name, string city)
        //     => View("Result", $"{name} lives in the city called {city}");
        [HttpPost]
        public RedirectToActionResult ReceiveForm(string name, string city)
        {
             TempData["name"] = name;
             TempData["city"] = city;
                     return   RedirectToAction(nameof(Data));
        }
        public ViewResult Data()
        {
            string name = TempData["name"] as string;
            string city = TempData["city"] as string;
                    return View("Result", $"{name} lives in {city}");
        }
    }
}
