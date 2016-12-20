using System.Web.Mvc;
using LogManagement.Filters;

namespace LogManagement
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LogActionFilterAttribute());
            filters.Add(new LogExceptionFilterAttribute());
        }
    }
     
}
