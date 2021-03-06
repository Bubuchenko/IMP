﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using System.Configuration;
using System.Web.Routing;

[assembly: OwinStartup("ConfigureSignalR", typeof(IMP_Service.ConfigureSignalR))]

namespace IMP_Service
{
    public class ConfigureSignalR
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    EnableDetailedErrors = true
                };
                map.RunSignalR(hubConfiguration);
            });
        }
    }
}
