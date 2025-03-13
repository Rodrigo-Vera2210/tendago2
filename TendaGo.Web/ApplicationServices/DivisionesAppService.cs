using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using RestSharp;
using TendaGo.Common;
using TendaGo.Web.Infraestructura;
using TendaGo.Web.Models;

namespace TendaGo.Web.ApplicationServices
{
    public class DivisionesAppService : AppServiceBase
    {
        public static List<DivisionDto> ObtenerDivisiones()
        {
            var divisions = new List<DivisionDto>();
            var request = new RestRequest("/divisions/all", Method.GET);

            var restResponse = DefaultClient.Execute<List<DivisionDto>>(request);
            if (restResponse.IsSuccessful)
            {
                divisions = restResponse.Data;
            }
            return divisions;
        }

        public static List<DivisionDto> ObtenerDivisionesActivas()
        {
            var divisions = new List<DivisionDto>();
            var request = new RestRequest("/divisions", Method.GET);

            var restResponse = DefaultClient.Execute<List<DivisionDto>>(request);
            if (restResponse.IsSuccessful)
            {
                divisions = restResponse.Data;
            }
            return divisions;
        }


        public static DivisionDto ObtenerDivision(int id)
        {
            var division = new DivisionDto();
            var request = new RestRequest($"/divisions/{id}", Method.GET);
            
            var restResponse = DefaultClient.Execute<DivisionDto>(request);
            if (restResponse.IsSuccessful)
            {
                division = restResponse.Data;
            }
            return division;
        }


        public static DivisionDto GuardarDivision(DivisionDto model)
        {
            var request = new RestRequest("/divisions", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(model);
            var restResponse = DefaultClient.Execute<DivisionDto>(request);
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