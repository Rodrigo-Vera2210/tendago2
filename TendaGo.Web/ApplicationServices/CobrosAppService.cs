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
using System.Collections;
using System.Globalization;
using System.Web.SessionState;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TendaGo.Web.ApplicationServices
{
    public class CobrosAppService : AppServiceBase
    {
        public const string baseURL = "https://localhost:44302/";
        public static List<ReceivableDto> ObtenerCuotasPendientes(int idCliente)
        {
            var receivables = new List<ReceivableDto>();
            var request = new RestRequest($"/clients/{idCliente}/receivables", Method.GET);

            IRestResponse<List<ReceivableDto>> restResponse = DefaultClient.Execute<List<ReceivableDto>>(request);
            if (restResponse.IsSuccessful)
            {
                receivables = restResponse.Data;
            }
            return receivables;
        }

        public static async Task<HttpResponseMessage> DownloadCierreCajaAsync(int idEmpresa, int idLocal, string FechaDesde, string FechaHasta, string idCajero, string format = "PDF")
        {
            var model = new CierreCajaViewModel();
            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("app_token", AppToken);
            
            return await client.PostAsync($"{Tools.GetApiUrlBase()}/consultaCierre/download?idEmpresa={idEmpresa}&idLocal={idLocal}&FechaDesde={FechaDesde}" +
                $"&FechaHasta={FechaHasta}&IdCajero={idCajero}&format={format}", data);
        }

        public static ReceiptDto ReciboPorId(string id)
        {
            var request = new RestRequest($"/receipts/{id}", Method.GET);

            var restResponse = DefaultClient.Execute<ReceiptDto>(request);

            if (restResponse.IsSuccessful)
            {
                return restResponse.Data;
            }
                
            throw new ApplicationServicesException(restResponse.Content);
        }



        public static List<ReceiptSummaryDto> ResumenPagos(int? idLocal, DateTime? inicio = null, DateTime? fin = null, string idVendedor= null)
        {
            var receipts = new List<ReceiptSummaryDto>();
            var request = new RestRequest($"/receipts/summary?idLocal={idLocal}&desde={inicio:s}&hasta={fin:s}&idVendedor={idVendedor}", Method.GET);

            var restResponse = DefaultClient.Execute<List<ReceiptSummaryDto>>(request);
            if (restResponse.IsSuccessful)
            {
                receipts = restResponse.Data;
            }
            return receipts;
        }


        public static List<ReceiptDto> ConsultaRecibos(int idLocal, string search = null, DateTime? inicio = null, DateTime? fin = null)
        {
            var receipts = new List<ReceiptDto>();
            var request = new RestRequest($"/receipts/?search={search}&from={inicio}&end={fin}", Method.GET);

            var restResponse = DefaultClient.Execute<List<ReceiptDto>>(request);
            if (restResponse.IsSuccessful)
            {
                receipts = restResponse.Data;
            }
            return receipts;
        }
        public static List<ReceiptDto> SearchRec(int idLocal, ReciboSearchModel model)
        {
            List<ReceiptDto> receipts = new List<ReceiptDto>();
            //var request = new RestRequest($"/warehouses/{idLocal}/outputs/search", Method.GET);
            var request = new RestRequest($"/receipts/?search={model.term}&from={model.getFechaInicio()}&end={ model.getFechaFin()}&lsearch={true}", Method.GET);
            //request.AddOrUpdateParameter("idCustomer", model.idPersona);

            var restResponse = DefaultClient.Execute<List<ReceiptDto>>(request);
            if (restResponse.IsSuccessful)
            {
                receipts = restResponse.Data;
            }

            // No devuelve pedidos si hubo un error
            return receipts;
        }

        public static ReceiptDto ReciboReversar(string id)
        {
            ReceiptDto receipts = new ReceiptDto();
            var request = new RestRequest($"/receipts/reverseReceipt?id={id}", Method.POST);
            var restResponse = DefaultClient.Post<ReceiptDto>(request);

            if (restResponse.IsSuccessful)
            {
                receipts = restResponse.Data;
            }
            return receipts;
        }

        public static CierreCajaViewModel GuardarCierreCaja(CashBalanceRequestModel model)
        {
            // Ahora asignamos los valores predeterminados:
             
            var request = new RestRequest("/cash/balance", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(model);
            request.Timeout = Convert.ToInt32(new TimeSpan(0, 5, 0).TotalMilliseconds);
            var restResponse = DefaultClient.Execute<CierreCajaViewModel>(request);

            if (restResponse.IsSuccessful)
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

        public static ReceiptDto GuardarCobranza(CobroDebitoViewModel model, int idlocal)
        {
            // Ahora asignamos los valores predeterminados:
            var json = model.ToJson();
            var requestModel = model.ToReceiptDto();
            var requestjson = requestModel.ToJson();

            requestModel.CreatedBy = User.Identity.Name;
            requestModel.CreatedOn = DateTime.Now;
            requestModel.IpCreator = AppServiceBase.ObtenerIp();

            //var client = new RestClient(baseURL);
            var request = new RestRequest($"/receipts/{idlocal}", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(requestModel);
            request.Timeout = Convert.ToInt32(new TimeSpan(0, 5, 0).TotalMilliseconds);
            var restResponse = ReceiptClient.Execute<ReceiptDto>(request);
            //var restResponse = client.Execute<ReceiptDto>(request);

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

    }
}