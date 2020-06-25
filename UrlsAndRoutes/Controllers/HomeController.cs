using Microsoft.AspNetCore.Mvc;
using System;
using UrlsAndRoutes.Infrastructure;

namespace UrlsAndRoutes.Controllers
{
    [Profile]
    [ViewResultDetails]
    [RangeException]
    public class HomeController : Controller
    {
        //public ViewResult Index() => View("Message","This is the Index action on the Home controller");
        //public IActionResult Index()
        //{
        //    if (!Request.IsHttps)
        //    {
        //        return new StatusCodeResult(StatusCodes.Status403Forbidden);
        //    }
        //    else
        //    {
        //        return View("Message",
        //        "This is the Index action on the Home controller");
        //    }
        //}
        
        public ViewResult Index() => View("Display","This is the Index action on the Home controller");
    
        public ViewResult SecondAction() => View("Display","This is the SecondAction action on the Home controller");
        public ViewResult GenerateException(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            else if (id > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            else
            {
                return View("Display", $"The value is {id}");
            }
        }
    }
}
