using DailySports.DataLayer.Context;
using DailySports.DataLayer.Model;
using DailySports.ServiceLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DailySports.BackOffice.Controllers
{

    public class HomeController : Controller
    {
        private DailySportsContext db = new DailySportsContext();
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl = "")
        {
            ViewBag.error = "";
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login()
        {
            try
            {
                if (ModelState.IsValid)
                {


                    string username = Request["username"];
                    string password = Request["password"];
                
                    var data = db.Users.Where(x => x.Name.ToLower() == username.ToLower()).FirstOrDefault();
                    bool pass = PasswordHelper.VerifyHash(password.ToLower(), "SHA512", data.Password);
                    if (data != null && !string.IsNullOrWhiteSpace(username))
                    {
                        //if (!pass)
                        //{
                        //    ViewBag.errormsg = "The user name or password provided is incorrect.";
                        //    return View();
                        //}
                        FormsAuthentication.SetAuthCookie(username, createPersistentCookie: false);
                        return Redirect("~/News");
                    }
                    else if (data == null)
                    {
                        ViewBag.errormsg = "The user name or password provided is incorrect.";
                        return View();
                    }
                    FormsAuthentication.SetAuthCookie(username, createPersistentCookie: false);
                    return Redirect("~/News");
                }
            }
            catch (Exception ex)
            {
                ViewBag.errormsg = ex.Message;
            }
            ViewBag.ReturnUrl = Request["returnUrl"];
            return View();

        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            return Redirect("~/home/index");
        }

    }
}