using Microsoft.Owin.Hosting;
using OpenNETCF.FormManager;
using OpenNETCF.IoC;
using Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace FormManagerService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            HostAsApp();
        }

        private static void HostAsApp()
        {
            new ManagerHost().Start();

            while (true)
            {
                Task.WaitAll(Task.Delay(1000));
                Debug.Write(".");
            }
        }

        private static void HostAsService()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ManagerService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }

    internal class ManagerHost
    {
        public ManagerHost()
        {
        }

        public void Start()
        {
            // TODO: get config for root URL and data location
            var store = new FormsStore();

            RootWorkItem.Services.Add<IFormStore>(store);

            WebApp.Start<ManagerSetup>("http://localhost:8080");
        }
    }

    internal class ManagerSetup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();

            //  Enable attribute based routing
            config.MapHttpAttributeRoutes();
/*
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api",
                defaults: new { id = RouteParameter.Optional }
            );
*/
            appBuilder.UseWebApi(config);
        }
    }
}
