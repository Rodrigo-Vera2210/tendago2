using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using RestSharp;
using TendaGo.Common;
using TendaGo.Web.Infraestructura;
using TendaGo.Web.Models;

namespace TendaGo.Web.ApplicationServices
{
    public class MarcasAppService : AppServiceBase
    {
        public static List<BrandDto> ObtenerMarcas()
        {
            var brands = new List<BrandDto>();
            var request = new RestRequest("/brands", Method.GET);
            var restResponse = DefaultClient.Execute<List<BrandDto>>(request);
            if (restResponse.IsSuccessful)
            {
                brands = restResponse.Data;
            }
            return brands;
        }

        public static BrandDto ObtenerMarca(int id)
        {
            var brand = new BrandDto();

            var request = new RestRequest($"/brands/{id}", Method.GET);
            
            var restResponse = DefaultClient.Execute<BrandDto>(request);
            if (restResponse.IsSuccessful)
            {
                brand = restResponse.Data;
            }
            return brand;
        }

        public static BrandDto GuardarMarca(BrandDto model)
        {
            var request = new RestRequest("/brands", Method.POST);

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(model);

            var restResponse = DefaultClient.Execute<BrandDto>(request);

            if (restResponse.IsSuccessful)
            {
                return restResponse.Data;
            }

            var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse.Content);

            if (string.IsNullOrEmpty(errorResponse.TechnicalMessage))
            {
                var genericApiResponse = JsonConvert.DeserializeObject<GeneriApiErrorResponse>(restResponse.Content);
                throw new ApplicationServicesException(genericApiResponse.Message);
            }

            throw new ApplicationServicesException(errorResponse);
        }
    }
}