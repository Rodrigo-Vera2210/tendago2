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
    public class CategoriasAppService : AppServiceBase
    {
        public static CategoryDto ObtenerCategoria(int id)
        {
            var cat = new CategoryDto();
            
            var request = new RestRequest($"/categories/{id}", Method.GET);

            var restResponse = DefaultClient.Execute<CategoryDto>(request);

            if (restResponse.IsSuccessful)
            {
                cat = restResponse.Data;
            }

            return cat;
        }

        public static List<CategoryDto> ObtenerCategorias(int idLinea)
        {
            var categories = new List<CategoryDto>();

            var request = new RestRequest($"/lines/{idLinea}/categories/all", Method.GET);

            IRestResponse<List<CategoryDto>> restResponse = DefaultClient.Execute<List<CategoryDto>>(request);

            if (restResponse.IsSuccessful)
            {
                categories = restResponse.Data;
            }
            return categories;
        }

        public static List<CategoryDto> ObtenerCategoriasActivas(int idLinea)
        {
            var categories = new List<CategoryDto>();
            
            var request = new RestRequest($"/lines/{idLinea}/categories", Method.GET);

            IRestResponse<List<CategoryDto>> restResponse = DefaultClient.Execute<List<CategoryDto>>(request);
            
            if (restResponse.IsSuccessful)
            {
                categories = restResponse.Data;
            }

            return categories;
        }

        public static CategoryDto GuardarCategoria(CategoryDto model)
        {
            var request = new RestRequest("/categories", Method.POST);

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(model);

            var restResponse = DefaultClient.Execute<CategoryDto>(request);
            
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