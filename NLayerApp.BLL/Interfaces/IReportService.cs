﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayerApp.BLL.DTO;
namespace NLayerApp.BLL.Interfaces
{
    public interface IReportService
    {
        void MakeChanges(ReportDTO reportDto);
        void SetReport(ReportDTO reportDto);
        void DeleteReport(ReportDTO reportDto);
        ReportDTO GetReport(int? id);
        IEnumerable<ReportDTO> GetReports();
        void Dispose();
    }
}