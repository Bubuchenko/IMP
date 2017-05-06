using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace IMP_Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                name: "FindBy1",
                routeTemplate: "Client/FindBy1/{antivirusstatus}/{status}/{systemtype}"
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{parameter}",
                defaults: new { parameter = RouteParameter.Optional }
            );
        }
    }
}
