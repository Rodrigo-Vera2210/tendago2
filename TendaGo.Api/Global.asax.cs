//using System;
//using System.Configuration;
//using System.Web.Http;

//namespace TendaGo.Api
//{
//    public class WebApiApplication
//    {
//        protected void Application_Start()
//        {
//            GlobalConfiguration.Configure(WebApiConfig.Register);
//        }


//        protected void Application_Error()
//        {
//            try
//            {
//                //var ex = Server.GetLastError();

//                ////log an exception
//                //var defaultEmail = ConfigurationManager.AppSettings["TendaGo:Email"];
//                //var url = Request.Url;
//                //var form = Request.Form.ToHtml();

//                //var user = User.ToUser().ToJson();

//                //Tools.SendMail(defaultEmail, $"WEB TendaGo: Reporte de Error {DateTime.Now}",
//                //          $"<p>Fecha: {DateTime.Now}</p>"
//                //        + $"<p>URL: {url}</p> "
//                //        + $"<p>Form: <br>{form}</p>"
//                //        + $"<p>Ocurrio un error al procesar un comando en la web.</p> "
//                //        + $"<p>Informacion del Usuario: {user}</p> <pre>{ex}</pre>");

//            }
//            catch { }

//        }

//    }
//}
