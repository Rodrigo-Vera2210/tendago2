using Newtonsoft.Json;
using TendaGo.Web.Infraestructura;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TendaGo.Web.ApplicationServices;

namespace TendaGo.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            log4net.Config.XmlConfigurator.Configure();
        }


        protected void Application_Error()
        {
            try
            {
                var ex = Server.GetLastError();

                // Descarto los errores organicos de apple.
                if (!$"{ex}".Contains("apple"))
                {
                    //log an exception
                    var defaultEmail = ConfigurationManager.AppSettings["TendaGo:Email"];
                    var url = Request.Url;
                    var ip = AppServiceBase.ObtenerIp();
                    var form = Request.Form.ToHtml();

                    var user = JsonConvert.SerializeObject(ApplicationServices.AppServiceBase.AppUser);

                    Tools.SendMail(defaultEmail, $"WEB TendaGo: Reporte de Error {DateTime.Now}",
                              $"<p>Fecha: {DateTime.Now}</p>"
                            + $"<p>URL: {url}</p> "
                            + $"<p>IP: {ip}</p> "
                            + $"<p>Form: <br>{form}</p>"
                            + $"<p>Ocurrio un error al procesar un comando en la web.</p> "
                            + $"<p>Informacion del Usuario: {user}</p> <pre>{ex}</pre>");

                }
            }
            catch { }

        }
    }
}
