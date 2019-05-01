using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using NLayerApp.BLL.Services;
using NLayerApp.BLL.Interfaces;

namespace NLayerApp.WEB.Util
{
    public class ReportModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IReportService>().To<ReportService>();
        }
    }
}