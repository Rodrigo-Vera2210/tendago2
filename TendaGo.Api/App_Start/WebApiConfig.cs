using System.Configuration;
using System.Web.Http;

namespace TendaGo.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var allowedClients = ConfigurationManager.AppSettings.Get("cors:UrlsAllowedClients");

            var cors = new EnableCorsAttribute(allowedClients, "*", "*");
            config.EnableCors(cors);

            // Web API configuration and services
            //config.Filters.Add(new TokenAuthorizeAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling =
            //    Newtonsoft.Json.DateTimeZoneHandling.Local;

            config.Routes.MapHttpRoute(
               name: "swagger_root",
               routeTemplate: "",
               defaults: null,
               constraints: null,
               handler: new RedirectHandler((message => message.RequestUri.ToString()), "swagger")
           );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
