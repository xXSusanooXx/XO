using System.Web;
using System.Web.Mvc;
using XO.Web.Filters;

namespace XO.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new XOExceptionFilter());
            filters.Add(new AuthorizeAttribute());
        }
    }
}