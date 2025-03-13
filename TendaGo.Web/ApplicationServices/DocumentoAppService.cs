using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions;
using TendaGo.Common;
using TendaGo.Web.Infraestructura;
using TendaGo.Web.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TendaGo.Web.ApplicationServices
{
    public class DocumentoAppService : AppServiceBase
    {
        public static DocumentDto GetFacturaById(string idFactura)
        {
            DocumentDto doc = new DocumentDto();
            var request = new RestRequest($"/documents/{idFactura}", Method.GET);
            var restResponse = DefaultClient.Execute<DocumentDto>(request);
            if (restResponse.IsSuccessful)
            {
                doc = restResponse.Data;
            }

            // No devuelve pedidos si hubo un error
            return doc;
        }


        public static DocumentDto GuardarDocumento(DocumentDto model)
        {
            var request = new RestRequest($"/document/create", Method.POST);
            
            request.AddJsonBody(model);

            var restResponse = DefaultClient.Execute<DocumentDto>(request);

            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                var result = restResponse.Data;
                return result;
            }

            var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse.Content);
            if (string.IsNullOrEmpty(errorResponse.TechnicalMessage))
            {
                var genericApiResponse = JsonConvert.DeserializeObject<GeneriApiErrorResponse>(restResponse.Content);
                throw new ApplicationServicesException(genericApiResponse.Message);
            }

            throw new ApplicationServicesException(errorResponse);
        }

        internal static async Task<HttpResponseMessage> DownloadFactura(string idFactura)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("app_token", AppToken);
                return await client.GetAsync($"{Tools.GetApiUrlBase()}/documents/{idFactura}/download");
            }
        }
    }
}