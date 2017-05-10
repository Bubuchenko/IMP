using IMP_Data.Repositories;
using IMP_Service.Hubs;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace IMP_Service.App_Code
{
    public class Initializer
    {
        //Runs before first request
        public static void AppInitialize()
        {
            SessionRepository.CloseAllActiveSessions().Wait();
            //Start SignalR server
            WebApp.Start(ConfigurationManager.AppSettings["SignalRHostAddress"]);

            var serializerSettings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            };

            serializerSettings.Converters.Add(new StringEnumConverter());

            var serializer = JsonSerializer.Create(serializerSettings);
            GlobalHost.DependencyResolver.Register(typeof(JsonSerializer), () => serializer);


            //Initialize binding constructor
            System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(HubEventBindings).TypeHandle);

        }
    }
}