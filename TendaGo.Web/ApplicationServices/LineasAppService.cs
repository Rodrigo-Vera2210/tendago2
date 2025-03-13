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
    public class LineasAppService : AppServiceBase
    {
        public static List<LineDto> ObtenerLineas(int idDivision)
        {
            var lines = new List<LineDto>();
            var request = new RestRequest($"/divisions/{idDivision}/lines/all", Method.GET);

            var restResponse = DefaultClient.Execute<List<LineDto>>(request);
            if (restResponse.IsSuccessful)
            {
                lines = restResponse.Data;
            }

            return lines;
        }

        public static List<LineDto> ObtenerLineasActivas(int idDivision)
        {
            var lines = new List<LineDto>();
            var request = new RestRequest($"/divisions/{idDivision}/lines", Method.GET);

            var restResponse = DefaultClient.Execute<List<LineDto>>(request);
            if (restResponse.IsSuccessful)
            {
                lines = restResponse.Data;
            }

            return lines;
        }

        public static LineDto ObtenerLinea(int id)
        {
            var line = new LineDto();

            var request = new RestRequest($"/lines/{id}", Method.GET);

            var restResponse = DefaultClient.Execute<LineDto>(request);
            if (restResponse.IsSuccessful)
            {
                line = restResponse.Data;
            }

            return line;
        }


        public static LineDto GuardarLinea(LineDto model)
        {
            var request = new RestRequest("/lines", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(model);
            
            var restResponse = DefaultClient.Execute<LineDto>(request);
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