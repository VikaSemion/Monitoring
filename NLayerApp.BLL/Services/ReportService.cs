using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayerApp.BLL.DTO;
using NLayerApp.DAL.Entities;
using NLayerApp.DAL.Interfaces;
using NLayerApp.BLL.Infrastructure;
using NLayerApp.BLL.Interfaces;
using AutoMapper;

namespace NLayerApp.BLL.Services
{
    public class ReportService : IReportService
    {
        IUnitOfWork Database { get; set; }

        public ReportService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void MakeChanges(ReportDTO ReportDto)
        {
            Report Report = Database.Reports.Get(ReportDto.Id);

            if (Report == null)
                throw new ValidationException("Report was not found", "");
            else {
                Report.Id = ReportDto.Id;
                Report.Date = ReportDto.Date;
                Report.City = ReportDto.City;
                Report.Worker = ReportDto.Worker;
                Report.O3 = ReportDto.O3;
                Report.NO2 = ReportDto.NO2;
                Report.SO2 = ReportDto.SO2;
                    }
            Database.Reports.Update(Report);
            Database.Save();
        }

        public void SetReport(ReportDTO ReportDto)
        {
            Report report = Database.Reports.Get(ReportDto.Id);
            Report Report = new Report
            {
            Id = ReportDto.Id,
            Date = ReportDto.Date,
            City = ReportDto.City,
            Worker = ReportDto.Worker,
            O3 = ReportDto.O3,
            NO2 = ReportDto.NO2,
            SO2 = ReportDto.SO2
            };
            Database.Reports.Create(Report);
            Database.Save();
        }

        public void AddToArchive(ReportDTO ReportDto)
        {
            Report Report = Database.Reports.Get(ReportDto.Id);
            Report report = Report.ShallowCopy();
            Database.Reports.Create(report);
            Database.Save();
        }

        public void DeleteReport(ReportDTO ReportDto)
        {
            Report Report = Database.Reports.Get(ReportDto.Id);
            Database.Reports.Delete(Report);
            Database.Save();
        }

        public IEnumerable<ReportDTO> GetReports()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Report, ReportDTO>()).CreateMapper();
            //var config = new MapperConfiguration(cfg =>
            //cfg.CreateMap<Report, ReportDTO>().ForMember(dest => dest.AZTRDs, opt => opt.Ignore());
            return mapper.Map<IEnumerable<Report>, List<ReportDTO>>(Database.Reports.GetAll());
        }


        public ReportDTO GetReport(int? id)
        {
            if (id == null)
                throw new ValidationException("Can`t find report with the same ID", "");
            var Report = Database.Reports.Get(id.Value);
            if (Report == null)
                throw new ValidationException("Report was not found", "");

            return new ReportDTO { Id = Report.Id, Date = Report.Date, City = Report.City, Worker = Report.Worker,
            O3 = Report.O3, NO2 = Report.NO2, SO2 = Report.SO2};
        }



        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
