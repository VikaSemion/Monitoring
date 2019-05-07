using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.BLL.DTO
{
    public class ReportDTO
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string City { get; set; }
        public string Worker { get; set; }
        public int O3 { get; set; }
        public int NO2 { get; set; }
        public int SO2 { get; set; }


        public ReportDTO ShallowCopy()
        {
            return (ReportDTO)this.MemberwiseClone();
        }


    }
}