using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using RestSharp;
using TendaGo.Common;
using TendaGo.Web.Infraestructura;
using TendaGo.Web.Models;

namespace TendaGo.Web.ApplicationServices
{
    public class ClientesAppService : AppServiceBase
    {
        public static ClientDto ObtenerClientePorId(int idCliente)
        {
            var categories = new ClientDto();
            var request = new RestRequest($"/clients/{idCliente}", Method.GET);
            IRestResponse<ClientDto> restResponse = DefaultClient.Execute<ClientDto>(request);
            if (restResponse.IsSuccessful)
            {
                categories = restResponse.Data;
            }
            return categories;
        }


        public static ClientDto GuardarCliente(ClientDto cliente)
        {
            var request = new RestRequest("/clients", Method.POST);
            request.AddJsonBody(cliente);
            var restResponse = DefaultClient.Execute<ClientDto>(request);

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


        public static List<EntityDto> BuscarClientes(string searchTerm = null, bool identification = false, bool lite = false, int? page = null)
        {
            var clientes = new List<EntityDto>();
            var request = new RestRequest($"/clients/search/{searchTerm}?identification={identification}&lite={lite}&page={page}", Method.GET);
            var restResponse = DefaultClient.Execute<List<EntityDto>>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                clientes = restResponse.Data;
            }
            return clientes;
        }

        public static List<EntityDto> BuscarClientesCrear(string identification = null)
        {
            var clientes = new List<EntityDto>();
            var request = new RestRequest($"/clients/consulta?id={identification}", Method.GET);
            var restResponse = DefaultClient.Execute<List<EntityDto>>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                clientes = restResponse.Data;
            }
            return clientes;
        }

        public static async Task<HttpResponseMessage> DownloadClientesAsync(FacturaViewModels model, string format = "PDF")
        {

            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("app_token", AppToken);

            return await client.PostAsync($"{Tools.GetApiUrlBase()}/consultaClientes/download?format={format}", data);
        }
    }
}