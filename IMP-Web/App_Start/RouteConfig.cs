using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IMP_Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "Login",
                url: "Login",
                defaults: new { controller = "Auth", action = "Login" }
            );

            routes.MapRoute(
                name: "ControlPanel",
                url: "{controller}/{action}",
                defaults: new { controller = "{controller}", action = "{action}" }
            );

            routes.MapRoute(
                name: "FileUpload",
                url: "File/Upload/{file}",
                defaults: new { controller = "File", action = "Upload" }
            );

            routes.MapRoute(
                name: "FileDownload",
                url: "File/Download/{filename}",
                defaults: new { controller = "File", action = "Download" }
            );

            routes.MapRoute(
                name: "FileBrowser",
                url: "{controller}/{action}/{clientID}/{Drive}",
                defaults: new { controller = "Control", action = "FileBrowser" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Auth", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
