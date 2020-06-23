using Microsoft.AspNetCore.Mvc;
using UrlsAndRoutes.Controllers;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.ReceiveForm("Andrew", "Seattle");
            Assert.Equal("Result", result.ViewName);
        }
    }
}
