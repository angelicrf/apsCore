using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfriguringApps.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ConfriguringApps.Controllers
{
    public class HomeController : Controller
    {
        private UptimeService uptime;
        public HomeController(UptimeService up) => uptime = up;
        public IActionResult Index()
                   => View(new Dictionary<string, string>
                   {
                       ["Message"] = "This is from the Home controller /Index",
                       ["Uptime"] = $"{uptime.Uptime} millissecond"
                   });
    }
}
