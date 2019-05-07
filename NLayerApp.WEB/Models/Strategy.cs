using NLayerApp.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NLayerApp.WEB.Models
{

    public interface IReport2
    {
        int Calculate(IEnumerable<ReportDTO> reports);
    }

    public class CalculatorO3 : IReport2
    {
        public int Calculate(IEnumerable<ReportDTO> reports) =>
            reports
                .Select(r => r.O3)
                .Sum();


    }

    public class CalculatorNO2 : IReport2
    {
        public int Calculate(IEnumerable<ReportDTO> reports) =>
            reports
                .Select(r => r.NO2)
                .Sum();


    }

    public class CalculatorSO2 : IReport2
    {
        public int Calculate(IEnumerable<ReportDTO> reports) =>
            reports
                .Select(r => r.SO2)
                .Sum();


    }

    public class Report2
    {
        private IReport2 _calculator;

        public Report2(IReport2 calculator)
        {
            _calculator = calculator;
        }

        public void SetCalculator(IReport2 calculator) => _calculator = calculator;

        public int Calculate(IEnumerable<ReportDTO> reports) => _calculator.Calculate(reports);
    }
}