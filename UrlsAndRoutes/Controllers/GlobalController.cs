using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UrlsAndRoutes.Controllers
{
    public class GlobalController : Controller
    {
        public ViewResult Index() => View("Display","This is the global controller");
    }
}
