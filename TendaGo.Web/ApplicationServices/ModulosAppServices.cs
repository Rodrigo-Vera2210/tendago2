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
    public class ModulosAppServices : AppServiceBase
    {
        public static List<ModuleDto> ObtenerModulos()
        {
            var modulos = new List<ModuleDto>();
            var request = new RestRequest("/modules", Method.GET);

            var restResponse = DefaultClient.Execute<List<ModuleDto>>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                modulos = restResponse.Data;
            }
            return modulos;
        }
    }
}