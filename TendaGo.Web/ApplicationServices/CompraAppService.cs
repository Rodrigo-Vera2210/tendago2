using Newtonsoft.Json;
using RestSharp;
using TendaGo.Common;
using TendaGo.Web.Infraestructura;
using TendaGo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Threading.Tasks;
using System.Net.Http;

namespace TendaGo.Web.ApplicationServices
{
    public class CompraAppService : AppServiceBase
    {
        public static InputDto SaveInput(InputDto compraDto)
        {
            var request = new RestRequest("/input/create", Method.POST);
            request.AddJsonBody(compraDto);
            var restResponse = DefaultClient.Execute<InputDto>(request);

            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                var result = restResponse.Data;
                return result;
            }

            var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse.Content);
            if (string.IsNullOrEmpty(errorResponse?.TechnicalMessage))
            {
                var genericApiResponse = JsonConvert.DeserializeObject<GeneriApiErrorResponse>(restResponse.Content);
                throw new ApplicationServicesException(genericApiResponse?.Message ?? restResponse.ErrorMessage ?? "Hubo un error al guardar la compra.");
            }

            throw new ApplicationServicesException(errorResponse);
        }

        public static InputDto GetInputById(string id)
        {
            InputDto result = default;
            var request = new RestRequest($"/input/{id}", Method.GET);
            var restResponse = DefaultClient.Execute(request);
            if (restResponse.IsSuccessful)
            {
                // Agrego la definicion original de la orden a la sesión
                //Session[id] = result = restResponse.Data;

                result = JsonConvert.DeserializeObject<InputDto>(restResponse.Content);
            }

            // No devuelve pedidos si hubo un error
            return result;
        }

        public static ValidationInputResultModel ValidateProduct(string codProducto, string tipoUnidad, string idLocal)
        {
            var request = new RestRequest($"/inputs/validate-product?productCode={codProducto}&unitType={tipoUnidad}&local={idLocal}", Method.GET);
            var restResponse = DefaultClient.Execute<ValidationInputResultModel>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                return restResponse.Data;
            }

            return new ValidationInputResultModel ();
        }

        public static bool DeleteInput(InputDeleteDto dto)
        {
            var request = new RestRequest($"/input/{dto.IdEntrada}/delete", Method.POST);
            request.AddJsonBody(dto);

            var restResponse = DefaultClient.Post<bool>(request);

            if (restResponse.IsSuccessful)
            {
                return restResponse.Data;
            }

            var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse.Content);
            if (string.IsNullOrEmpty(errorResponse?.TechnicalMessage))
            {
                var genericApiResponse = JsonConvert.DeserializeObject<GeneriApiErrorResponse>(restResponse.Content);
                if (string.IsNullOrEmpty(genericApiResponse?.Message))
                {
                    throw restResponse.ErrorException;
                }
                throw new ApplicationServicesException(genericApiResponse?.Message);
            }

            throw new ApplicationServicesException(errorResponse);
        }

        public static async Task<HttpResponseMessage> DownloadInputAsync(string id, string format = "PDF", string type = "")
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("app_token", AppToken);

            return await client.GetAsync($"{Tools.GetApiUrlBase()}/input/{id}/download?format={format}&type={type}");
        }

    }
}