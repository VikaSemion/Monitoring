using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NLayerApp.BLL.Interfaces;
using NLayerApp.BLL.DTO;
using NLayerApp.WEB.Models;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using NLayerApp.BLL.Infrastructure;
using Microsoft.Owin;
using NLayerApp.BLL.Services;
using System.Net;

namespace NLayerApp.WEB.Controllers
{
    public class HomeController : Controller
    {

        IReportService ReportService;
        public HomeController(IReportService serv)
        {
            ReportService = serv;
        }


        public ActionResult Index()
        {
            IEnumerable<ReportDTO> ReportDtos = ReportService.GetReports();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ReportDTO, ReportViewModel>()).CreateMapper();
            var Reports = mapper.Map<IEnumerable<ReportDTO>, List<ReportViewModel>>(ReportDtos);
            //email = Request.QueryString["email"];
            var calculatorContext = new Report2(new CalculatorO3());
            var O3Total = calculatorContext.Calculate(ReportDtos);
            calculatorContext.SetCalculator(new CalculatorNO2());
            var NO2Total = calculatorContext.Calculate(ReportDtos);
            calculatorContext.SetCalculator(new CalculatorSO2());
            var SO2Total = calculatorContext.Calculate(ReportDtos);
            ViewBag.O3Result = O3Total;
            ViewBag.NO2Result = NO2Total;
            ViewBag.SO2Result = SO2Total;

            AbstractClass result = new Formula1();
            result.Calculating(O3Total, NO2Total, SO2Total);
            ViewBag.F1S1 = result.FirstStep(O3Total, NO2Total, SO2Total);
            ViewBag.F1S2 = result.SecondStep(O3Total, NO2Total, SO2Total);
            ViewBag.F1S3 = result.ThirdStep(O3Total, NO2Total, SO2Total);
            result = new Formula2();
            result.Calculating(O3Total, NO2Total, SO2Total);
            ViewBag.F2S1 = result.FirstStep(O3Total, NO2Total, SO2Total);
            ViewBag.F2S2 = result.SecondStep(O3Total, NO2Total, SO2Total);
            ViewBag.F2S3 = result.ThirdStep(O3Total, NO2Total, SO2Total);
            return View(Reports);
        }


        public ActionResult Archive(ReportDTO Report)
        {
            ReportService.AddToArchive(Report);
            IEnumerable<ReportDTO> ReportDtos = ReportService.GetReports();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ReportDTO, ReportViewModel>()).CreateMapper();
            var Reports = mapper.Map<IEnumerable<ReportDTO>, List<ReportViewModel>>(ReportDtos);
            string WorkerE = Request.QueryString["email"];
            return RedirectToAction("", "Home/Index", new { email = WorkerE });
        }

        public ActionResult MakeChanges(int? id)
        {
            try
            {
                ReportDTO Report = ReportService.GetReport(id);
                //string WorkerE = Request.QueryString["email"];
                var r = new ReportViewModel
                {
                    Id = Report.Id,
                    Date = Report.Date,
                    City = Report.City,
                    //Worker = WorkerE,
                    O3 = Report.O3,
                    NO2 = Report.NO2,
                    SO2 = Report.SO2
                };

                return View(r);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult MakeChanges(ReportViewModel Report)
        {
            try
            {
                string WorkerE = Request.QueryString["email"];
                var reportDto = new ReportDTO
                {
                    Id = Report.Id,
                    Date = Report.Date,
                    City = Report.City,
                    Worker = WorkerE,
                    O3 = Report.O3,
                    NO2 = Report.NO2,
                    SO2 = Report.SO2
                };
                ReportService.MakeChanges(reportDto);
                return RedirectToAction("", "Home/Index", new { email = WorkerE });
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(Report);
        }
        public ActionResult SetReport(int? id)
        {
            try
            {
                //string WorkerE = Request.QueryString["email"];
                var r = new ReportViewModel();
                //r.Worker = WorkerE;
                return View(r);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }





       
        public ActionResult GetReport(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(400, "BadRequest");
            ReportDTO report = ReportService.GetReport(id.Value);
            if (report == null)
                return new HttpStatusCodeResult(404, "NotFound");
            return View(report);
        }

        public ActionResult AddPhone() => View();

        [HttpPost]
        public ActionResult AddReport(ReportDTO report)
        {
            if (ModelState.IsValid)
            {
                ReportService.SetReport(report);
                return RedirectToAction("Index");
            }
            return View(report);
        }


        [HttpPost]
        public ActionResult SetReport(ReportViewModel Report)
        {
            try
            {
                string WorkerE = Request.QueryString["email"];
                var reportDto = new ReportDTO
                {
                    Id = Report.Id,
                    Date = Report.Date,
                    City = Report.City,
                    Worker = WorkerE,
                    O3 = Report.O3,
                    NO2 = Report.NO2,
                    SO2 = Report.SO2
                };
                ReportService.SetReport(reportDto);
                return RedirectToAction("", "Home/Index", new { email = WorkerE });
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(Report);
        }
        public ActionResult DeleteReport(int? id)
        {
            try
            {
                ReportDTO Report = ReportService.GetReport(id);
                var r = new ReportViewModel();

                return View(r);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult DeleteReport(ReportViewModel Report)
        {
            try
            {
                string WorkerE = Request.QueryString["email"];
                var reportDto = new ReportDTO
                {
                    Id = Report.Id
                };
                ReportService.DeleteReport(reportDto);
                return RedirectToAction("", "Home/Index", new { email = WorkerE });
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(Report);
        }

        protected override void Dispose(bool disposing)
        {
            ReportService.Dispose();
            base.Dispose(disposing);
        }


        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, Worker = model.Worker };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("", "Home/Login");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

         IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Information is not correct.");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if (String.IsNullOrEmpty(returnUrl))
                        //return RedirectToAction("", "Home/Index");
                     return Redirect(String.Format("Index?email={0}", model.Email));
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }
       /* public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }*/

    }
}