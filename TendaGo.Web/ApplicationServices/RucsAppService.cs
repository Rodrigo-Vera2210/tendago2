using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using RestSharp;
using TendaGo.Common;
using TendaGo.Web.Infraestructura;

namespace TendaGo.Web.ApplicationServices
{
    public class RucsAppService : AppServiceBase
    {
        public static List<RucDtoLite> ObtenerRucs()
        {
            List<RucDtoLite> rucs = new List<RucDtoLite>();
            var request = new RestRequest("/ruc", Method.GET);
            var restResponse = DefaultClient.Execute<List<RucDtoLite>>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                rucs = restResponse.Data;
            }
            return rucs;
        }

        public static RucDtoLite ObtenerRuc(string ruc)
        {
            var request = new RestRequest($"/ruc/{ruc}", Method.GET);
            var restResponse = DefaultClient.Execute<RucDto>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                return restResponse.Data;
            }

            return default;
        }
    }
}