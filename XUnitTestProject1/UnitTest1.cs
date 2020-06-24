using Microsoft.AspNetCore.Mvc;
using Moq;
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

            var mock = new Mock<IRepository>();
            mock.SetupGet(m => m.Products).Returns(data);

           var mock2 = new Mock<ProductTotalizer>();
           mock2.SetupGet(t => t.Total).Returns((decimal)343.54);

                //HomeController controller = new HomeController
                //{
                //    Repository = mock.Object
                //};
                //TypeBroker.SetTestObject(mock.Object);
                //HomeController controller = new HomeController();
             HomeController controller = new HomeController(mock.Object,mock2.Object);
             ViewResult result = controller.Index(pt);

             Assert.Equal(result.ViewData.Model, result.ViewData.Model);
        }
    }
}
