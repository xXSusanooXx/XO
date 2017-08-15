using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using XO.BLL;
using XO.Web.Models;
using XO.Web.XOProviders;

namespace XO.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {

        private readonly UsersBll dbUsers = new UsersBll();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (Membership.ValidateUser(model.Login, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Login, model.RememberMe);
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "XO");
            }

            ModelState.AddModelError("", "Имя пользователя или пароль не верны");
            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {

            if (dbUsers.Get(model.Login) != null)
            {
                ModelState.AddModelError("", "Пользователь с таким именем уже существует");
                return View(model);
            }

            if (model.Password.Length <= 2)
            {
                ModelState.AddModelError("", "Пароль должен быть больше двух символов");
                return View(model);
            }

            if (model.Password != model.PasswordConfirmation)
            {
                ModelState.AddModelError("", "Пароли не совпадают");
                return View(model);
            }

            var user = ((XoMembershipProvider)Membership.Provider).CreateUser(model.Login, model.Password);

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(model.Login, false);
                return RedirectToAction("Index", "XO");
            }

            ModelState.AddModelError("", "Fatal register error");
            return View(model);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}
