

namespace AppTest
{
    class Testing2
    {
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
