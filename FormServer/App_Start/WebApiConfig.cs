using OpenNETCF.FormManager;
using OpenNETCF.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace FormServer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            RootWorkItem.Services.AddNew<FormsStore>();
        }
    }
}
