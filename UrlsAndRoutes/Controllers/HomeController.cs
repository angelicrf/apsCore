﻿using Microsoft.AspNetCore.Mvc;
using UrlsAndRoutes.Models;
using static UrlsAndRoutes.Models.ProductRepository;

namespace UrlsAndRoutes.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository repository;
        public HomeController(IProductRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index() => View(repository.Products);
        public ViewResult Create() => View();
        [HttpPost]
        public IActionResult Create(Product newProduct)
        {
            repository.AddProduct(newProduct);
            return RedirectToAction("Index");
        }

    }
}
