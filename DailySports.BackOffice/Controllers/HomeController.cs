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

                    bool isAuthenticated = username.ToLower() == "admin" && password.ToLower() == "temppassword" ? true : false;

                    if (!isAuthenticated)
                    {
                        ViewBag.errormsg = "The Username you specified is not completely registered! Please contact IT.";
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

    }
}