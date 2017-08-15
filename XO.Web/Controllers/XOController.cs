using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XO.BLL;

namespace XO.Web.Controllers
{
    public class XOController : Controller
    {
        //
        // GET: /XO/
        private UsersBll dbUsers;

        public XOController()
        {
            dbUsers = new UsersBll();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}
