using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using TendaGo.Common;
using TendaGo.Web.Models;

namespace TendaGo.Web.ApplicationServices
{
    public class BodegaAppServices : AppServiceBase
    {
        public static OutputDto RevisarNotaPedido(SalesOrderReviewDto notaPedido)
        {
            return UpdateOutput("review", notaPedido.Id, notaPedido);
        }

        private static OutputDto UpdateOutput(string action, string id, object notaPedidoDto)
        {
            var request = new RestRequest($"/warehouses/{id}/{action}", Method.POST);
            request.AddJsonBody(notaPedidoDto);
            request.Timeout = (int)TimeSpan.FromMinutes(5).TotalMilliseconds;

            var restResponse = DefaultClient.Execute<OutputDto>(request);

            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                var result = restResponse.Data;
                return result;
            }

            Log("SaveOutput", restResponse.Content);

            var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse.Content);
            if (errorResponse != null)
            {
                throw new ApplicationServicesException(errorResponse);
            }

            var genericApiResponse = JsonConvert.DeserializeObject<GeneriApiErrorResponse>(restResponse.Content);
            throw new ApplicationServicesException(genericApiResponse?.Message ?? restResponse.Content);
        }

    }
}