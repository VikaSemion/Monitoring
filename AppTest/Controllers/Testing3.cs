using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NLayerApp.BLL.DTO;
using NLayerApp.WEB.Controllers;
using NLayerApp.BLL.Interfaces;
using NLayerApp.WEB.Models;
using Moq;
using Microsoft.Owin.Security;
using NLayerApp.BLL.Services;
using System.Web.Http.Results;
using NUnit.Framework;
using System.Web;
using System.Collections.Specialized;
using System.Web.Routing;


/// NUNIT FRAMEWORK

namespace AppTest.Controllers
{
    [TestFixture]
    public static class Test3
    {

        public static Mock<HttpContextBase> MockHttpContext(string path)
        {
            var mockHttpCtx = new Mock<HttpContextBase>();
            var mockReq = new Mock<HttpRequestBase>();
            mockReq.Setup(x => x.AppRelativeCurrentExecutionFilePath)
                   .Returns(path);
            mockReq.Setup(x => x.Headers).Returns(new NameValueCollection());
            mockReq.Setup(x => x.Cookies).Returns(new HttpCookieCollection());
            mockReq.Setup(x => x.QueryString).Returns(new NameValueCollection());
            mockReq.Setup(x => x.Form).Returns(new NameValueCollection());
            mockHttpCtx.Setup(x => x.Request).Returns(mockReq.Object);

            var mockResp = new Mock<HttpResponseBase>();
            mockResp.Setup(x => x.ApplyAppPathModifier(It.IsAny<string>()))
                    .Returns<string>(x => x);
            mockResp.Setup(x => x.Cookies).Returns(new HttpCookieCollection());
            mockHttpCtx.Setup(x => x.Response).Returns(mockResp.Object);

            return mockHttpCtx;
        }

        public static Mock<HttpContextBase> MockControllerContext(
                              this Controller controller, string path = null)
        {
            var mockHttpCtx = Test3.MockHttpContext(path);
            var requestCtx = new RequestContext(mockHttpCtx.Object, new RouteData());
            var controllerCtx = new ControllerContext(requestCtx, controller);
            controller.ControllerContext = controllerCtx;
            return mockHttpCtx;
        }

        [Test]
        public static void  MakeChangesPost_ReturnsARedirect_WhenModelStateIsValid()
        {
            // Arrange
            var mockRepo = new Mock<IReportService>();
            var controller = new HomeController(mockRepo.Object);
            ReportViewModel rep = new ReportViewModel();
            var ctx = controller.MockControllerContext();
            ctx.Object.Request.QueryString.Set("email", "semionviktoria@gmail.com");
            // ContextMocks.Request.SetupGet(r => r["mymodel"]).Returns(email);

            // Act
            System.Web.Mvc.RedirectToRouteResult redirectResult =
     (System.Web.Mvc.RedirectToRouteResult)controller.MakeChanges(rep);

            // Assert
            Assert.IsInstanceOf<System.Web.Mvc.RedirectToRouteResult>(redirectResult);
            Assert.AreEqual("Home/Index", redirectResult.RouteValues["controller"]);
        }

        [Test]
        public static void SetReportPost_ReturnsARedirect_WhenModelStateIsValid()
        {
            // Arrange
            var mockRepo = new Mock<IReportService>();
            var controller = new HomeController(mockRepo.Object);
            ReportViewModel rep = new ReportViewModel();
            var ctx = controller.MockControllerContext();
            ctx.Object.Request.QueryString.Set("email", "semionviktoria@gmail.com");
            // ContextMocks.Request.SetupGet(r => r["mymodel"]).Returns(email);

            // Act
            System.Web.Mvc.RedirectToRouteResult redirectResult =
     (System.Web.Mvc.RedirectToRouteResult)controller.SetReport(rep);

            // Assert
            Assert.IsInstanceOf<System.Web.Mvc.RedirectToRouteResult>(redirectResult);
            Assert.AreEqual("Home/Index", redirectResult.RouteValues["controller"]);
        }

        [Test]
        public static void DeleteReportPost_ReturnsARedirect_WhenModelStateIsValid()
        {
            // Arrange
            var mockRepo = new Mock<IReportService>();
            var controller = new HomeController(mockRepo.Object);
            ReportViewModel rep = new ReportViewModel();
            var ctx = controller.MockControllerContext();
            ctx.Object.Request.QueryString.Set("email", "semionviktoria@gmail.com");
            // ContextMocks.Request.SetupGet(r => r["mymodel"]).Returns(email);

            // Act
            System.Web.Mvc.RedirectToRouteResult redirectResult =
     (System.Web.Mvc.RedirectToRouteResult)controller.DeleteReport(rep);

            // Assert
            Assert.IsInstanceOf<System.Web.Mvc.RedirectToRouteResult>(redirectResult);
            Assert.AreEqual("Home/Index", redirectResult.RouteValues["controller"]);
        }
    }


}
