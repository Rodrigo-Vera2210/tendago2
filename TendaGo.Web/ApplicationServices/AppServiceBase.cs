using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using RestSharp;
using TendaGo.Common;
using TendaGo.Web.Infraestructura;
using TendaGo.Web.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using System.Web.SessionState;
using System.Net.Http;
using System.Net.Http.Json;
using ZXing.Aztec.Internal;

namespace TendaGo.Web.ApplicationServices
{
    public class AppServiceBase
    {
        static string PANTALLAS_SESSION => $"Pantallas_{AppToken}";
        static string EMPRESA_SESSION => $"Empresa_{AppToken}";
        static string AppToken_SESSION => $"AppToken";

        public static List<DisplayDto> Permisos
        {
            get
            {
                if (Session[PANTALLAS_SESSION] == null)
                {
                    Session[PANTALLAS_SESSION] = PantallasAppService.ObtenerPantallasPerfil();
                }

                return Session[PANTALLAS_SESSION] as List<DisplayDto>;
            }
        }
        public static CompanyDto Empresa
        {
            get
            {
                if (Session[EMPRESA_SESSION] == null)
                {
                    Session[EMPRESA_SESSION] = UserAppService.ObtenerEmpresa();
                }

                return Session[EMPRESA_SESSION] as CompanyDto;
            }
        }

        protected static HttpSessionState Session => HttpContext.Current?.Session;


        public static HttpClient GetHttpClient()
        {                                        
            var result = Session[$"HttpClient_{AppToken}"] as HttpClient;
            if (result == null)
            {
                Session[$"HttpClient_{AppToken}"] = result = new HttpClient();

                // Siempre agrego el encabezado con el token de seguridad
                if (!result.DefaultRequestHeaders.Contains("app_token"))
                    result.DefaultRequestHeaders.Add("app_token", AppToken);
            }


            return result;

        }

        public static T GetAction<T>(string uri)
        {
            var httpClient = GetHttpClient();

            var response = httpClient.GetAsync($"{DefaultClient.BaseUrl}/{uri}").Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadFromJsonAsync<T>().Result;
                return result;
            }

            var json = response.Content.ReadAsStringAsync().Result;

            var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(json);
            if (errorResponse != null)
            {
                throw new ApplicationServicesException(errorResponse);
            }

            var genericApiResponse = JsonConvert.DeserializeObject<GeneriApiErrorResponse>(json);
            throw new ApplicationServicesException(genericApiResponse?.Message ?? json);

        }

        public static T DeleteAction<T>(string uri)
        {
            var httpClient = GetHttpClient();

            var response = httpClient.DeleteAsync($"{DefaultClient.BaseUrl}/{uri}").Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadFromJsonAsync<T>().Result;
                return result;
            }

            var json = response.Content.ReadAsStringAsync().Result;

            var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(json);
            if (errorResponse != null)
            {
                throw new ApplicationServicesException(errorResponse);
            }

            var genericApiResponse = JsonConvert.DeserializeObject<GeneriApiErrorResponse>(json);
            throw new ApplicationServicesException(genericApiResponse?.Message ?? json);

        }

        public static T PutAction<T>(string uri, object content = default)
        {
            var httpClient = GetHttpClient();

            var response = httpClient.PutAsJsonAsync($"{DefaultClient.BaseUrl}/{uri}", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadFromJsonAsync<T>().Result;
                return result;
            }

            var json = response.Content.ReadAsStringAsync().Result;

            var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(json);
            if (errorResponse != null)
            {
                throw new ApplicationServicesException(errorResponse);
            }

            var genericApiResponse = JsonConvert.DeserializeObject<GeneriApiErrorResponse>(json);
            throw new ApplicationServicesException(genericApiResponse?.Message ?? json);

        }

        public static T PostAction<T>(string uri, object content = default)
        {
            var httpClient = GetHttpClient();

            var response = httpClient.PostAsJsonAsync($"{DefaultClient.BaseUrl}/{uri}", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadFromJsonAsync<T>().Result;
                return result;
            }

            var json = response.Content.ReadAsStringAsync().Result;

            var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(json);
            if (errorResponse != null)
            {
                throw new ApplicationServicesException(errorResponse);
            }

            var genericApiResponse = JsonConvert.DeserializeObject<GeneriApiErrorResponse>(json);
            throw new ApplicationServicesException(genericApiResponse?.Message ?? json);

        }

        public static RestClient DefaultClient
        {
            get
                {
                if (!(Session[$"RestClient_{AppToken}"] is RestClient result))
                {
                    Session[$"RestClient_{AppToken}"] = result = GetApiClient();

                    // Siempre agrego el encabezado con el token de seguridad
                    result.AddDefaultParameter("app_token", AppToken, ParameterType.HttpHeader);
                }


                return result;
            }
        }

        #region NUEVAS APIS
        public static RestClient OutputClient
        {
            get
            {
                //var url = "https://localhost:44302";
                //var url = "https://tendago-salida-aps-desa.azurewebsites.net";
                var url = "https://tendago-outputapi.binasystem.com";

                if (!(Session[$"RestClient_{ApiToken}"] is RestClient result))
                {
                    Session[$"RestClient_{ApiToken}"] = result = new RestClient(url);
                    // Siempre agrego el encabezado con el token de seguridad
                    result.AddDefaultParameter("Authorization", $"Bearer {ApiToken}", ParameterType.HttpHeader);

                }
                else if (result != null && result.BaseUrl.ToString() != url)
                {
                    Session[$"RestClient_{ApiToken}"] = result.BaseUrl = new Uri(url);
                }

                return result;
            }
        }

        public static RestClient ReceiptClient
        {
            get
            {
                //var url = "https://localhost:44302";
                //var url = "https://tendago-cuentasporcobrar-aps-desa.azurewebsites.net";
                var url = "https://tendago-receiptapi.binasystem.com";

                if (!(Session[$"RestClient_{ApiToken}"] is RestClient result))
                {
                    Session[$"RestClient_{ApiToken}"] = result = new RestClient(url);
                    // Siempre agrego el encabezado con el token de seguridad
                    result.AddDefaultParameter("Authorization", $"Bearer {ApiToken}", ParameterType.HttpHeader);

                }
                else if (result != null && result.BaseUrl.ToString() != url)
                {
                    Session[$"RestClient_{ApiToken}"] = result.BaseUrl = new Uri(url);
                }

                return result;
            }
        }

        public static RestClient CompanyClient
        {
            get
            {
                //var url = "https://localhost:44302";
                var url = "https://tendago-empresa-aps.azurewebsites.net";
           

                if (!(Session[$"RestClient_{ApiToken}"] is RestClient result))
                {
                    Session[$"RestClient_{ApiToken}"] = result = new RestClient(url);
                    // Siempre agrego el encabezado con el token de seguridad
                    result.AddDefaultParameter("Authorization", $"Bearer {ApiToken}", ParameterType.HttpHeader);

                }
                else if (result != null && result.BaseUrl.ToString() != url)
                {
                    Session[$"RestClient_{ApiToken}"] = result.BaseUrl = new Uri(url);
                }

                return result;
            }
        }

        public static RestClient AuthClient
        {
            get
            {
                var url = "https://api.binasystem.com/auth/v1/";

                if (!(Session[$"RestClient_{ApiToken}"] is RestClient result))
                {
                    Session[$"RestClient_{ApiToken}"] = result = new RestClient(url);
                    // Siempre agrego el encabezado con el token de seguridad
                    result.AddDefaultParameter("Authorization", $"Bearer {ApiToken}", ParameterType.HttpHeader);
                }
                else if (result != null && result.BaseUrl.ToString() != url)
                {
                    Session[$"RestClient_{ApiToken}"] = result.BaseUrl = new Uri(url);
                }

                return result;
            }
        }


        public static RestClient FacturaGOClient
        {
            get
            {
                var url = "https://api.binasystem.com/facturago/comprobantes";

                if (!(Session[$"RestClient_{ApiToken}"] is RestClient result))
                {
                    Session[$"RestClient_{ApiToken}"] = result = new RestClient(url);
                    // Siempre agrego el encabezado con el token de seguridad
                    result.AddDefaultParameter("Authorization", $"Bearer {ApiToken}", ParameterType.HttpHeader);

                }
                else if (result != null && result.BaseUrl.ToString() != url)
                {
                    Session[$"RestClient_{ApiToken}"] = result.BaseUrl = new Uri(url);
                }

                return result;
            }
        }

        #endregion




        protected static RestClient GetApiClient()
        {
            var client = new RestClient(Tools.GetApiUrlBase());
            
            return client;
        }

        public static Usuario AppUser
        {
            get
            {
                if (User?.Identity != null && User.Identity.IsAuthenticated)
                {
                    Session["USER"] = Usuario.Actual;
                }

                return Session["USER"] as Usuario;
            }
            set { Session["USER"] = value; }
        }

        public static string AppToken
        {
            get
            {
                if (User?.Identity != null && User.Identity.IsAuthenticated)
                {
                    Session["TOKEN"] = User.Identity.GetTokenUsuario();
                }

                return Session["TOKEN"] as string;
            }
            set { Session["TOKEN"] = value; }
        }

        public static string ApiToken
        {
            get
            {
                if (User?.Identity != null && User.Identity.IsAuthenticated)
                {
                    Session["TOKEN"] = User.Identity.GetAuthTokenUsuario();
                }

                return Session["TOKEN"] as string;
            }
            set { Session["TOKEN"] = value; }
        }

        public static string ObtenerIp()
        {
            // The following method returns the client IP address. It is better to use this method than the Request.UserHostAddress() 
            // because UserHostAddress sometimes may capture the IP address of user's proxy.
            // Ref: https://www.c-sharpcorner.com/blogs/get-client-ip-address-in-mvc-30

            string ip = HttpContext.Current?.Request?.ServerVariables?["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ip))
            {
                ip = HttpContext.Current?.Request?.ServerVariables?["REMOTE_ADDR"];
            }

            return ip ?? HttpContext.Current?.Request?.UserHostAddress ?? "127.0.0.1";
        }


        internal static void Log(string method, params object[] parameters)
        {
            var builder = new StringBuilder();
            
            builder.AppendLine(new string('=', 50));
            builder.AppendLine($"Metodo: {method}");
            builder.AppendLine($"Fecha: {DateTime.Now}");
            builder.AppendLine();

            foreach (var item in parameters)
            {
                if (item is Exception)
                {
                    builder.AppendLine($"{(item as Exception)}");
                }
                else if (item is object)
                {
                    builder.AppendLine($"{JsonConvert.SerializeObject(item)}");
                }
                else
                {
                    builder.AppendLine($"{item}");
                }
            }

            File.AppendAllText(HttpContext.Current.Server.MapPath($"~/log-{DateTime.Now:yyyy-MM-dd}.txt"), $"{builder}");
        }

        public static IPrincipal User { get { return HttpContext.Current.User; } }
    }
}