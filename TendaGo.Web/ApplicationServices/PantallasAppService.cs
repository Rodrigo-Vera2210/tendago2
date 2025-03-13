using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using RestSharp;
using TendaGo.Common;
using TendaGo.Web.Infraestructura;

namespace TendaGo.Web.ApplicationServices
{
    public class PantallasAppService : AppServiceBase
    {
        public static List<DisplayDto> ObtenerPantallasPerfil()
        {
            var pantallas = new List<DisplayDto>();
            var request = new RestRequest("/display/profile", Method.GET);
            request.Timeout = Convert.ToInt32(TimeSpan.FromMinutes(5).TotalMilliseconds);
            var restResponse = DefaultClient.Execute<List<DisplayDto>>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                pantallas = restResponse.Data;
            }
            return pantallas;
        }
    }
}