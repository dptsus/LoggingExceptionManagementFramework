using System;
using System.Diagnostics;
using System.Globalization;
using System.Web.Mvc;
using LogManagement.Core;
using LogManagement.Models;

namespace LogManagement.Filters
{
    public sealed class LogExceptionFilterAttribute : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {

            //ILogManager dbLogger = new DbLogManager();
            //dbLogger.LogExceptions(new ExceptionEntry
            //{
            //    Exception = filterContext.Exception.ToString(),
            //    Title = Constants.ApplicationName
            //});

            ILogManager fileLogger = new FileLogManager();
            fileLogger.LogExceptions(new ExceptionEntry
            {
                Title = Constants.ApplicationName,
                Message = filterContext.Exception.Message, 
                Id = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Priority = "Medium",
                SessionId = Guid.NewGuid().ToString(),
                Severity = TraceEventType.Error.ToString(),
                Exception = filterContext.Exception.ToString(),
                StackStrace = filterContext.Exception.StackTrace,
                IpAddress = filterContext.HttpContext.Request.UserHostAddress,
            }); 
        }

    }
}