using System.Collections.Generic;

namespace UrlsAndRoutes.Models
{
    public interface IRepository
    {
        IEnumerable<City> Cities { get; }
        void AddCity(City newCity);
    }
}