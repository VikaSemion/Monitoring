using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;
using NLayerApp.BLL.DTO;
using NLayerApp.WEB.Controllers;
using NLayerApp.BLL.Interfaces;
using NLayerApp.WEB.Models;
using Moq;
using Microsoft.Owin.Security;
using NLayerApp.BLL.Services;
using System.Web.Http.Results;
using NLayerApp.DAL.Entities;

namespace AppTest
{
    public class Testing2
    {
        [Fact]
        public void GetReportReturnsBadRequestResultWhenIdIsNull()
        {
            // Arrange
            var mock = new Mock<IReportService>();
            var controller = new HomeController(mock.Object);

            // Act
            var result = controller.GetReport(null) as HttpStatusCodeResult;

            // Arrange
            Assert.IsType<HttpStatusCodeResult>(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("BadRequest", result.StatusDescription);
        }

        [Fact]
        public void GetReportReturnsNotFoundResultWhenReportNotFound()
        {
            int ReportId = 1;
            var mock = new Mock<IReportService>();
            mock.Setup(repo => repo.GetReport(ReportId))
                .Returns(null as ReportDTO);
            var controller = new HomeController(mock.Object);

            var result = controller.GetReport(ReportId) as HttpStatusCodeResult;

            Assert.IsType<HttpStatusCodeResult>(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("NotFound", result.StatusDescription);
        }

        [Fact]
        public void GetReportReturnsViewResultWithReport()
        {
            int ReportId = 2;
            var mock = new Mock<IReportService>();
            mock.Setup(repo => repo.GetReport(ReportId))
                .Returns(GetTestReports().FirstOrDefault(p => p.Id == ReportId));
            var controller = new HomeController(mock.Object);

            var result = controller.GetReport(ReportId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<ReportDTO>(viewResult.ViewData.Model);
            Assert.Equal("20.02.2019", model.Date);
            Assert.Equal("Kherson", model.City);
            Assert.Equal("OlegButin@gmail.com", model.Worker);
            Assert.Equal(200, model.O3);
            Assert.Equal(100, model.NO2);
            Assert.Equal(89, model.SO2);
            Assert.Equal(ReportId, model.Id);
        }
    }
}
