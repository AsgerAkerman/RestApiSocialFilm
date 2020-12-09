using RestApiSocialFilm.UserAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace RestApiSocialFilm
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            // Web API routes
            config.MapHttpAttributeRoutes();
            //Filter som findes i userauthentocation
            config.Filters.Add(new RequireHttpsAttribute());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "cinemano/",
                defaults: new { id = RouteParameter.Optional }

            ); 
            
        }

    }
}
