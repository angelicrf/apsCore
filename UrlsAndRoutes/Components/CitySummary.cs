using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlsAndRoutes.Models;
using static UrlsAndRoutes.Models.CityRepository;

namespace UrlsAndRoutes.Components
{
    public class CitySummary : ViewComponent
    {
        private ICityRepository repository;
        public CitySummary(ICityRepository repo)
        {
            repository = repo;
        }
        //public IViewComponentResult Invoke()
        //{
        //    // return $"{repository.Cities.Count()} cities, "
        //    // + $"{repository.Cities.Sum(c => c.Population)} people";
        //    //return View(new CityViewModel
        //    //{
        //    //    Cities = repository.Cities.Count(),
        //    //    Population = repository.Cities.Sum(c => c.Population)
        //    //});
        //    string target = RouteData.Values["id"] as string;

        //    var cities = repository.Cities.Where(city => target == null || string.Compare(city.Country, target, true) == 0);

        //    return View(new CityViewModel
        //    {
        //        Cities = cities.Count(),
        //        Population = cities.Sum(c => c.Population)
        //    });
        //}
        public IViewComponentResult Invoke(bool showList)
        {
            if (showList)
            {
                return View("CityList", repository.Cities);
            }
            else
            {
                return View(new CityViewModel
                {
                    Cities = repository.Cities.Count(),
                    Population = repository.Cities.Sum(c => c.Population)
                });
            }
        }
    }
}
