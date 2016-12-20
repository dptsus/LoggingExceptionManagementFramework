using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using LogManagement.Core;
using LogManagement.Models;

namespace LogManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                var fm = new FileLogManager();
                fm.LogExceptions(new ExceptionEntry
                { 
                    Title = Constants.ApplicationName,
                    Message = ex.Message,
                    Id = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    Priority = "Medium",
                    SessionId = Guid.NewGuid().ToString(),
                    Severity = TraceEventType.Error.ToString(),
                    Exception = fm.CreateExceptionString(ex),
                    StackStrace = ex.StackTrace
                });
            } 
            
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}