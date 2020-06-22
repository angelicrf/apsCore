using Microsoft.AspNetCore.Mvc;
using UrlsAndRoutes.Areas.Admin.Models;

namespace UrlsAndRoutes.Areas.Admin
{
        [Area("Admin")]
        public class HomeController : Controller
        {
            public Person[] data = new Person[] {
                    new Person { Name = "Alice", City = "London" },
                    new Person { Name = "Bob", City = "Paris" },
                    new Person { Name = "Joe", City = "New York" }
                    };
            public ViewResult Index() => View(data);
        }
 }

