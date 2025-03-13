using Newtonsoft.Json;
using RestSharp;
using TendaGo.Common;
using TendaGo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace TendaGo.Web.ApplicationServices
{
    public class ProveedorAppService : AppServiceBase
    { 
        public static List<ProviderDto> ConsultarProveedores(string searchTerm, bool identification = false)
        {
            var request = new RestRequest($"/providers/search/{searchTerm}?identification={identification}", Method.GET);
            var restResponse = DefaultClient.Execute<List<ProviderDto>>(request);

            if (restResponse.IsSuccessful)
            {
                List<ProviderDto> providers = restResponse.Data;
                return providers;
            }

            var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse.Content);
            if (string.IsNullOrEmpty(errorResponse?.TechnicalMessage))
            {
                var genericApiResponse = JsonConvert.DeserializeObject<GeneriApiErrorResponse>(restResponse.Content);
                throw new ApplicationServicesException(genericApiResponse?.Message);
            }

            throw new ApplicationServicesException(errorResponse);
        }


        public static ProviderDto GuardarProveedor(ProviderDto provider)
        {
            var request = new RestRequest("/providers", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddJsonBody(provider);
            var restResponse = DefaultClient.Execute<ProviderDto>(request);
            if (restResponse.IsSuccessful)
            {
                var providerSaved = restResponse.Data;
                return providerSaved;
            }

            var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse.Content);
            if (string.IsNullOrEmpty(errorResponse?.TechnicalMessage))
            {
                var genericApiResponse = JsonConvert.DeserializeObject<GeneriApiErrorResponse>(restResponse.Content);
                throw new ApplicationServicesException(genericApiResponse?.Message);
            }

            throw new ApplicationServicesException(errorResponse);
        }
    }
}