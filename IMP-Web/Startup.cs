using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using IMP_Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using IMP_Data;
using IMP_Web.App_Start;

[assembly: OwinStartup(typeof(IMP_Web.Startup))]

namespace IMP_Web
{
    public class Startup
    {
        public static Func<UserManager<User>> UserManagerFactory { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(IMPContext.Create);
            app.CreatePerOwinContext<UserManager>(UserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login")
            });

            UserManagerFactory = () =>
            {
                var usermanager = new UserManager<User>(
                    new UserStore<User>(new IMPContext()));

                usermanager.UserValidator = new UserValidator<User>(usermanager)
                {
                    RequireUniqueEmail = true
                };

                return usermanager;
            };

        }
    }
}
