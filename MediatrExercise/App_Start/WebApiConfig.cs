using Autofac;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MediatrExercise
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.MapHttpAttributeRoutes();
            config.Routes.ConfigureApiRoutes();
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        internal static void ConfigureApiRoutes(this HttpRouteCollection routes)
        {
            
            routes.MapHttpRoute(
                name: "GetLogs",
                routeTemplate: "api/getAllLogs",
                defaults: new { controller = "LogsApi", action = "GetAllLogs", id = RouteParameter.Optional });


        }
    }
}
