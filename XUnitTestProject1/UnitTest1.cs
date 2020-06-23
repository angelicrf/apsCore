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
            RedirectToActionResult result = controller.ReceiveForm("Andrew", "Seattle");
            Assert.Equal("Data", result.ActionName);
        }
        [Fact]
        public void ModelObjectType()
        {
        
            ExampleController controller = new ExampleController();
            ViewResult result = controller.Index();
            Assert.IsType<System.DateTime>(result.ViewData.Model);
            Assert.IsType<string>(result.ViewData["Message"]);
            Assert.Equal("Hello", result.ViewData["Message"]);
            Assert.IsType<System.DateTime>(result.ViewData["Date"]);
            //Assert.Equal(new[] { "Alice", "Bob", "Joe" }, result.Value);
            //Assert.Equal(404, result.StatusCode);

        }
        [Fact]
        public void Redirection()
        {
            ExampleController controller = new ExampleController();
            RedirectToRouteResult result = controller.Redirect();
           // Assert.Equal("/Example/Index", result.Url);
            Assert.False(result.Permanent);
            Assert.Equal("Example", result.RouteValues["controller"]);
            Assert.Equal("Index", result.RouteValues["action"]);
            Assert.Equal("MyID", result.RouteValues["ID"]);
        }
        [Fact]
        public void Redirection2()
        {
           
            ExampleController controller = new ExampleController();            
            RedirectToActionResult result = controller.Redirect2();
            Assert.False(result.Permanent);
            Assert.Equal("Index", result.ActionName);
        }
    }
}
