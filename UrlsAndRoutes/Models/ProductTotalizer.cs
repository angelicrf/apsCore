using System;
using System.Collections.Generic;
using System.Linq;

namespace UrlsAndRoutes.Models
{
    public class ProductTotalizer
    {
        public ProductTotalizer() { }
        public ProductTotalizer(IRepository repo) => Repository = repo;
        public IRepository Repository { get; set; }
        public decimal Total => Repository.Products.Sum(p => p.Price);
    }
}
