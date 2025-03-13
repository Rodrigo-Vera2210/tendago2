using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using RestSharp;
using TendaGo.Common;
using TendaGo.Web.Infraestructura;
using Newtonsoft.Json;
using TendaGo.Web.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using log4net.Repository.Hierarchy;
using log4net.Core;
using NPOI.SS.Formula.Functions;
using System.Text;
using System.Net.Http.Json;

namespace TendaGo.Web.ApplicationServices
{
    public class NotaPedidoAppService : AppServiceBase
    {
        public static OutputDto GetOutputById(string id)
        {
            var request = new RestRequest($"/output/{id}", Method.GET);
            request.Timeout = (int)TimeSpan.FromMinutes(5).TotalMilliseconds;
            var restResponse = OutputClient.Execute<OutputDto>(request);

            if (restResponse.IsSuccessful)
            {
                // Agrego la definicion original de la orden a la sesión
                var output = restResponse.Data;
                
                Session[id] = output;
                return output;
            }

            // No devuelve pedidos si hubo un error
            return default;
        }

        public static List<OutputDto> GetOutputsByCurrentStatus(TransactionStatus currentStatus, string transactionType=null)
        {
            List<OutputDto> notasPedido = new List<OutputDto>();
            var request = new RestRequest($"/output;status={(int)currentStatus}?transactionType={transactionType}", Method.GET);
            var restResponse = DefaultClient.Execute<List<OutputDto>>(request);

            if (restResponse.IsSuccessful)
            {
                notasPedido = restResponse.Data;
            }

            // No devuelve pedidos si hubo un error
            return notasPedido;
        }


        public static List<OutputDto> GetOutputsByLocal(int idLocal, TransactionStatus currentStatus, TransactionType? transactionType =null)
        {
            List<OutputDto> notasPedido = new List<OutputDto>();
            var request = new RestRequest($"warehouses/{idLocal}/outputs/{currentStatus}?transactionType={transactionType}", Method.GET);
            var restResponse = DefaultClient.Execute<List<OutputDto>>(request);
            
            if (restResponse.IsSuccessful)
            {
                notasPedido = restResponse.Data;
            }

            // No devuelve pedidos si hubo un error
            return notasPedido;
        }

        public static List<OutputDto> GetOutputs()
        {
            List<OutputDto> notasPedido = new List<OutputDto>();
            var request = new RestRequest($"/output", Method.GET);
            var restResponse = DefaultClient.Execute<List<OutputDto>>(request);
            if (restResponse.IsSuccessful)
            {
                notasPedido = restResponse.Data;
            }

            // No devuelve pedidos si hubo un error
            return notasPedido;
        }


        public static bool DeleteOutput(OutputDeleteDto dto)
        {
            var request = new RestRequest($"/output/{dto.IdSalida}/delete", Method.POST);
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

        public static OutputDto SaveOutput(OutputDto notaPedidoDto)
        {
            var request = new RestRequest("/output/create", Method.POST);
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
            if (errorResponse!=null)
            {
                throw new ApplicationServicesException(errorResponse);
            }

            var genericApiResponse = JsonConvert.DeserializeObject<GeneriApiErrorResponse>(restResponse.Content);
            throw new ApplicationServicesException(genericApiResponse?.Message ?? restResponse.Content);
            
        }

        public static OutputDto AprobarNotaPedido(SalesOrderApprovalDto notaPedido)
        {
            return UpdateOutput("approve", notaPedido.Id, notaPedido);
        }
         
        public static OutputDto EmpaquetarNotaPedido(SalesOrderPackingDto notaPedido)
        {
            return UpdateOutput("packing", notaPedido.Id, notaPedido);
        }

        public static OutputDto RevisarNotaPedido(SalesOrderReviewDto notaPedido)
        {
            return UpdateOutput("review", notaPedido.Id, notaPedido);
        }

        public static OutputDto EntregarNotaPedido(SalesOrderDeliverDto notaPedido)
        {
            return UpdateOutput("deliver", notaPedido.Id, notaPedido);
        }

        public static OutputDto FacturarNotaPedido(SalesOrderInvoiceDto notaPedido)
        {
            return UpdateOutput("invoice", notaPedido.Id, notaPedido);
        }

        public static OutputDto NotaCreditoElectronica(SalesOrderInvoiceDto notaPedido)
        {
            return UpdateOutput("credit", notaPedido.Id, notaPedido);
        }

        private static OutputDto UpdateOutput(string action, string id, object notaPedidoDto)
        {
            var result = PostAction<OutputDto>($"/salesOrder/{id}/{action}", notaPedidoDto);
            return result;                                                                   
        }


        public static void SetDetail(string idDetalle, bool? empaquetado = null, bool? revisado = null)
        {
            var request = new RestRequest($"/output/detail/{idDetalle}?empaquetado={empaquetado}&revisado={revisado}", Method.POST);

            var restResponse = DefaultClient.Execute<OutputDto>(request);

            if (!restResponse.IsSuccessful)
            {
                var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse.Content);
                if (errorResponse != null)
                {
                    throw new ApplicationServicesException(errorResponse);
                }

                var genericApiResponse = JsonConvert.DeserializeObject<GeneriApiErrorResponse>(restResponse.Content);
                throw new ApplicationServicesException(genericApiResponse?.Message ?? restResponse.Content);
            }
        }



        public static async Task<HttpResponseMessage> DownloadOutputAsync(string id, string format = "PDF", string type = "")
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("app_token", AppToken);
            
            return await client.GetAsync($"{Tools.GetApiUrlBase()}/output/{id}/download?format={format}&type={type}");
        }

        public static async Task<HttpResponseMessage> DownloadOutputSlimAsync(string id, string format = "PDF", string type = "")
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("app_token", AppToken);

            return await client.GetAsync($"{Tools.GetApiUrlBase()}/output/{id}/ticket?format={format}&type={type}");
        }

        public static OutputDto SaveOutpuChanget(OutputDto notaPedidoDto,string idSalida,int IdLocalSelected)
        {
            var request = new RestRequest($"/output/{idSalida}/{IdLocalSelected}/changeLocal", Method.POST);
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

        public static bool ChangeCuentasPorCobrar(string id,string idNew)
        {
            var request = new RestRequest($"/receivables/{id}/{idNew}/receivables", Method.POST);
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

    }
}