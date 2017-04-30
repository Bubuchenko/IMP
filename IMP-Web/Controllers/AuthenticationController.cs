using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMP_Web.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
    }
}