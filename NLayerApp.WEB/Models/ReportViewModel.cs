using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NLayerApp.WEB.Models
{
    public class ReportViewModel
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string City { get; set; }
        public string Worker { get; set; }
        public int O3 { get; set; }
        public int NO2 { get; set; }
        public int SO2 { get; set; }

        public ReportViewModel ShallowCopy()
        {
            return (ReportViewModel)this.MemberwiseClone();
        }
    }
}