using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;
using UrlsAndRoutes.Controllers;
using UrlsAndRoutes.Infrastructure;
using UrlsAndRoutes.Models;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        //private IRepository rep;
        //private IModelStorage storage;
        public ProductTotalizer pt = new ProductTotalizer();
        public MemoryRepository tg = new MemoryRepository();

        [Fact]
        public void ControllerTest()
        {           
            var data = new[] { new Product { Name = "Test", Price = 100 } };
            ProductTotalizer yh =  new ProductTotalizer { Total = 890 };

            var mock = new Mock<IRepository>();
            mock.SetupGet(m => m.Products).Returns(data);

            //var mock2 = new Mock<ProductTotalizer>();

            // mock2.Setup(t => t.Total).Returns(data[0].Price);

            //HomeController controller = new HomeController
            //{
            //    Repository = mock.Object
            //};
            //TypeBroker.SetTestObject(mock.Object);
            //HomeController controller = new HomeController();
            
             HomeController controller = new HomeController(mock.Object,yh);
             ViewResult result = controller.Index(yh);

             Assert.Equal(result.ViewData.Model, result.ViewData.Model);
        }
    }
}
