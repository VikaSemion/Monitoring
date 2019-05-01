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

        [Fact]
        public void IndexReturnsAViewResultWithAListOfReports()
        {
            // Arrange
            var mock = new Mock<IReportService>();
            mock.Setup(repo => repo.GetReports()).Returns(GetTestReports());
            var controller = new HomeController(mock.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ReportViewModel>>(viewResult.Model);
            Assert.Equal(GetTestReports().Count, model.Count());
        }

        public List<ReportDTO> GetTestReports()
        {
            var reports = new List<ReportDTO>
            {

                 new ReportDTO { Id = 1, Date = "01.02.2019", City = "Kyiv", Worker = "JohnUolker@gmail.com",
                O3 = 220, NO2 = 160 , SO2 = 16 },
                new ReportDTO
            {
                Id = 2,
                Date = "20.02.2019",
                City = "Kherson",
                Worker = "OlegButin@gmail.com",
                O3 = 200,
                NO2 = 100,
                SO2 = 89
            },
            new ReportDTO
            {
                Id = 3,
                Date = "16.04.2019",
                City = "Poltava",
                Worker = "AnnaSavina@gmail.com",
                O3 = 250,
                NO2 = 89,
                SO2 = 65
            },
            new ReportDTO
            {
                Id = 4,
                Date = "18.03.2019",
                City = "Lviv",
                Worker = "OrnestLutyi@gmail.com",
                O3 = 180,
                NO2 = 96,
                SO2 = 91
            },
            new ReportDTO
            {
                Id = 5,
                Date = "19.01.2019",
                City = "Odessa",
                Worker = "TomHalper@gmail.com",
                O3 = 163,
                NO2 = 120,
                SO2 = 101
            },
        };
            return reports;
        }

    }
}
