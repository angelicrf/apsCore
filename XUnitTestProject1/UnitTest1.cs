using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Moq;
using System.Linq;
using UrlsAndRoutes.Controllers;
using UrlsAndRoutes.Infrastructure;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void TestHttpsFilter()
        {

            var httpRequest = new Mock<HttpRequest>();

            httpRequest.SetupSequence(m => m.IsHttps).Returns(true).Returns(false);

            var httpContext = new Mock<HttpContext>();

            httpContext.SetupGet(m => m.Request).Returns(httpRequest.Object);

            var actionContext = new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ActionDescriptor());

            var authContext = new AuthorizationFilterContext(actionContext, Enumerable.Empty<IFilterMetadata>().ToList());

            HttpsUseAttribute filter = new HttpsUseAttribute();

            filter.OnAuthorization(authContext);

            Assert.Null(authContext.Result);

            filter.OnAuthorization(authContext);

            Assert.IsType(typeof(StatusCodeResult), authContext.Result);

        }
    }

}