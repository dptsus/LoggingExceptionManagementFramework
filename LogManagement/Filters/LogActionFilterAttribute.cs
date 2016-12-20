using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LogManagement.Core;
using LogManagement.Models;
using Newtonsoft.Json;

namespace LogManagement.Filters
{
    public sealed class LogActionFilterAttribute : ActionFilterAttribute
    {
        private Stopwatch _stopwatch;

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            _stopwatch = new Stopwatch();
            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            var eventEntry = GetEventEntry(actionExecutedContext);
            var fileLogger = new FileLogManager();
            fileLogger.LogApplicationCalls(eventEntry);
            base.OnActionExecuted(actionExecutedContext);
        }
        private EventEntry GetEventEntry(ActionExecutedContext actionExecutedContext)
        { 
            return new EventEntry
            {
                Title = Constants.ApplicationName + "-" + actionExecutedContext.ActionDescriptor.ControllerDescriptor.ControllerName ,
                CallType = actionExecutedContext.ActionDescriptor.ActionName,
                ResponseTime = Convert.ToDecimal(_stopwatch.Elapsed.TotalSeconds),
                Status = "True",
                Id = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                SessionId = Guid.NewGuid().ToString(),
                DateTime = actionExecutedContext.HttpContext.Timestamp,
                IpAddress = actionExecutedContext.HttpContext.Request.UserHostAddress,
            };
        }
         
    } 
}