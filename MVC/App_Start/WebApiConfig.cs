using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MVC
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                 name: "Api1",
                 routeTemplate: "API/api/{controller}/{action}/{id}",
                 defaults: new { id = RouteParameter.Optional }
             );

            config.Routes.MapHttpRoute(
                name: "Api2",
                routeTemplate: "API/api/{controller}/{action}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Api3",
                routeTemplate: "API/api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
