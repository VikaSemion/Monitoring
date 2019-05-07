using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLayerApp.BLL.DTO;
using NLayerApp.WEB.Controllers;
using NLayerApp.BLL.Interfaces;
using NLayerApp.WEB.Models;
using Moq;
using Microsoft.Owin.Security;
using NLayerApp.BLL.Services;
using System.Web.Http.Results;

/// MSTEST FRAMEWORK

namespace AppTest
{
    [TestClass]
    public class MyTest
    {
        [TestMethod]
        public void SetReport_CheckResultOfPage()
        {
            // Arrange

            var serv = new Mock<IReportService>();
            var controller = new HomeController(serv.Object);
            // Act
            ViewResult result = controller.SetReport(2) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }



        [TestMethod]
        public void Login_CheckLingResultIfModelIsNull_Exceptions()
        {
            // Arrange
            var mock = new Mock<IReportService>();
            ApplicationUser model = new ApplicationUser();
            model = null;
            var controller = new HomeController(mock.Object);
            // Act
            var result = controller.Login() as ViewResult;
            // Assert
            Assert.AreEqual(String.Empty, result.ViewName);

        }

        [TestMethod]
        public void DeleteReport_CheckResultOfPage()
        {
            // Arrange

            var serv = new Mock<IReportService>();
            var controller = new HomeController(serv.Object);
            // Act
            ViewResult result = controller.DeleteReport(2) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }


        [TestMethod]
        public void MakeChanges_CheckResultOfPage()
        {
            // Arrange
            int ReportId = 2;
            var mock = new Mock<IReportService>();
            mock.Setup(repo => repo.GetReport(ReportId))
                .Returns(GetTestReports().FirstOrDefault(p => p.Id == ReportId));
            var controller = new HomeController(mock.Object);
            // Act
            ViewResult result = controller.MakeChanges(2) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }



        [TestMethod]
        public void Register_CheckResultOfPage()
        {
            // Arrange
            var serv = new Mock<IReportService>();
            var controller = new HomeController(serv.Object);
            // Act
            ViewResult result = controller.Register() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(string.Empty, result.ViewName);
        }


        [TestMethod]
        public void Login_CheckResultOfPage()
        {
            // Arrange
            var serv = new Mock<IReportService>();
            var controller = new HomeController(serv.Object);
            // Act
            ViewResult result = controller.Login() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [TestMethod]
        public void Index_CheckResultOfPage()
        {
            // Arrange

            var serv = new Mock<IReportService>();
            var controller = new HomeController(serv.Object);
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
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
