using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IMP_Web.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            //FormsAuthentication.SetAuthCookie("bubuchenko", true);

            if (Request.IsAuthenticated)
                return View("Loggedin");

            return View("Login");
        }
    }
}