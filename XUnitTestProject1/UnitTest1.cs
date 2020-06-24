using Microsoft.AspNetCore.Mvc;
using Moq;
using UrlsAndRoutes.Controllers;
using UrlsAndRoutes.Models;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void ControllerTest()
        {
            var data = new[] { new Product { Name = "Test", Price = 100 } };
            var mock = new Mock<IRepository>();
            mock.SetupGet(m => m.Products).Returns(data);
            HomeController controller = new HomeController
            {
                Repository = mock.Object
            };
            ViewResult result = controller.Index();
            Assert.Equal(data, result.ViewData.Model);
        }
    }
}
