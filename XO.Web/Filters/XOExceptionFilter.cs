using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XO.Web.Filters
{
    public class XOExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            Exception e = filterContext.Exception;
            while (e.InnerException != null)
                e = e.InnerException;

            var message = e.Message;
            var a = 2;
        }
    }
}