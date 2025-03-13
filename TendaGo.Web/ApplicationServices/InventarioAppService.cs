using Newtonsoft.Json;
using RestSharp;
using TendaGo.Common;
using TendaGo.Web.Infraestructura;
using TendaGo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Text;
using NPOI.SS.Formula.Functions;
using TendaGo.Common.EntitiesDto;
using System.IO;

namespace TendaGo.Web.ApplicationServices
{
    public class InventarioAppService : AppServiceBase
    {

        public static List<TransferDto> SearchTransfers(int idLocal, OrderSearchModel model)
        {
            List<TransferDto> result = new List<TransferDto>();
            var request = new RestRequest($"/warehouses/{idLocal}/transfer/search", Method.GET);
            request.AddOrUpdateParameter("term", model.filtro);
            request.AddOrUpdateParameter("idVendedor", model.idVendedor);
            request.AddOrUpdateParameter("idCliente", model.idPersona);
            request.AddOrUpdateParameter("startDate", model.getFechaInicio());
            request.AddOrUpdateParameter("endDate", model.getFechaFin());
            request.AddOrUpdateParameter("transactionType", model.tipoTransaccion);
            request.AddOrUpdateParameter("status", model.status);

            var restResponse = DefaultClient.Execute<List<TransferDto>>(request);

            if (restResponse.IsSuccessful)
            {
                result = restResponse.Data;
            }

            // No devuelve pedidos si hubo un error
            return result;
        }



        public static TransferDto GetTransferById(string id)
        {
            TransferDto resultado = null;
            var request = new RestRequest($"warehouses/transfer/{id}", Method.GET);
            var restResponse = DefaultClient.Execute<TransferDto>(request);

            if (restResponse.IsSuccessful)
            {
                resultado = restResponse.Data;
            }

            // No devuelve pedidos si hubo un error
            return resultado;
        }


        public static List<TransferDto> SaveTransfer(TransferRequest model)
        {
            var product = new ResultDto<List<TransferDto>>();
            var request = new RestRequest($"/warehouses/transfer", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(model);

            var restResponse = OutputClient.Execute<ResultDto<List<TransferDto>>>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                //product = restResponse.Data;
                product = JsonConvert.DeserializeObject<ResultDto<List<TransferDto>>>(restResponse.Content);
                return product.Result;
            }
             
            throw new ApplicationServicesException(restResponse.Data.Message);
        }


        public static List<LiteProductDto> SearchProductsByLocal(int idLocal, string searchTerm)
        {

            searchTerm = searchTerm.Replace("%2520", " ");//Por alguna razón en el proceso lo considera la URL

            var request = new RestRequest($"/warehouses/{idLocal}/products/search?term={searchTerm}", Method.GET);
            var restResponse = DefaultClient.Execute<List<LiteProductDto>>(request);
            if (restResponse.IsSuccessful)
            {
                return restResponse.Data;
            }
            return new List<LiteProductDto>();
        }

        public static List<OutputDto> GetOutputsByLocal(int idLocal, TransactionStatus currentStatus)
        {
            List<OutputDto> resultado = new List<OutputDto>();
            var request = new RestRequest($"warehouses/{idLocal}/outputs/{currentStatus}?transactionType={TransactionType.SalidaBodega}", Method.GET);
            var restResponse = DefaultClient.Execute<List<OutputDto>>(request);

            if (restResponse.IsSuccessful)
            {
                resultado = restResponse.Data;
            }

            // No devuelve pedidos si hubo un error
            return resultado;
        }

        public static InputDto GetInputById(string id)
        {
            InputDto result = new InputDto();
            var request = new RestRequest($"/input/{id}", Method.GET);
            var restResponse = DefaultClient.Execute<InputDto>(request);
            if (restResponse.IsSuccessful)
            {
                // Agrego la definicion original de la orden a la sesión
                Session[id] = result = restResponse.Data;
            }

            // No devuelve pedidos si hubo un error
            return result;
        }

        public static List<InputDto> GetInputsByLocal(int idLocal, TransactionStatus? currentStatus = null)
        {
            List<InputDto> resultado = new List<InputDto>();
            var request = new RestRequest($"warehouses/{idLocal}/inputs/{currentStatus}?transactionType={TransactionType.EntradaBodega}", Method.GET);
            var restResponse = DefaultClient.Execute<List<InputDto>>(request);

            if (restResponse.IsSuccessful)
            {
                resultado = restResponse.Data;
            }

            // No devuelve pedidos si hubo un error
            return resultado;
        }

        public static List<SalesProductDto> GetProductSales(int idLocal, DateTime? startDate = null, DateTime? endDate = null)
        {
            List<SalesProductDto> resultado = new List<SalesProductDto>();

            var request = new RestRequest($"products/sales?idLocal={idLocal}&startDate={startDate}&endDate={endDate}", Method.GET);
            var restResponse = DefaultClient.Execute<List<SalesProductDto>>(request);

            if (restResponse.IsSuccessful)
            {
                resultado = restResponse.Data;
            }

            // No devuelve pedidos si hubo un error
            return resultado;
        }


        public static List<WarehouseDto> ObtenerLocales(string inicioSesion = null)
        {
            inicioSesion = inicioSesion ?? User.Identity.Name;
            var locales = new List<WarehouseDto>();
            //var request = new RestRequest("/warehouses", Method.GET);
            //var restResponse = DefaultClient.Execute<List<WarehouseDto>>(request);
            var request = new RestRequest($"/locales/{inicioSesion}", Method.GET);
            var restResponse = CompanyClient.Execute<List<WarehouseDto>>(request);
            if (restResponse.IsSuccessful)
            {
                locales = restResponse.Data;                                
            }
            return locales;
        }

        public static List<WarehouseDto> ObtenerLocalesOrdenados(int localSele)
        {
            var locales = new List<WarehouseDto>();
            var localesOrd = new List<WarehouseDto>();

            var request = new RestRequest("/warehouses", Method.GET);
            var restResponse = DefaultClient.Execute<List<WarehouseDto>>(request);
            if (restResponse.IsSuccessful)
            {
                locales = restResponse.Data;
                
                var empresa = locales[0].IdEmpresa;
                WarehouseDto todos = new WarehouseDto();
                todos.Id = 0;
                todos.IdEmpresa = empresa;
                todos.Local = "TODOS";
                locales.Add(todos);

                localesOrd.Add(locales.Where(x => x.Id == localSele).FirstOrDefault());
                foreach (var loc in locales) 
                {
                    if (loc.Id != localSele)
                    {
                        localesOrd.Add(loc);
                    }
                }
            }
            
            return localesOrd;
        }

        public static List<InputDto> SearchInputs(int idLocal, OrderSearchModel model)
        {
            List<InputDto> notasPedido = new List<InputDto>();
            var request = new RestRequest($"/warehouses/{idLocal}/inputs/search", Method.GET);
            request.AddOrUpdateParameter("term", model.filtro);
            request.AddOrUpdateParameter("idVendedor", model.idVendedor);
            request.AddOrUpdateParameter("idProveedor", model.idPersona);
            request.AddOrUpdateParameter("startDate", model.getFechaInicio());
            request.AddOrUpdateParameter("endDate", model.getFechaFin());
            request.AddOrUpdateParameter("transactionType", model.tipoTransaccion);
            request.AddOrUpdateParameter("status", model.status);

            var restResponse = DefaultClient.Execute<List<InputDto>>(request);
            if (restResponse.IsSuccessful)
            {
                notasPedido = restResponse.Data;
            }

            // No devuelve pedidos si hubo un error
            return notasPedido;
        }

        public static List<OutputDto> SearchOutputs(int idLocal, OrderSearchModel model)
        {
            List<OutputDto> notasPedido = new List<OutputDto>();
            var request = new RestRequest($"/warehouses/{idLocal}/outputs/search", Method.GET);
            request.AddOrUpdateParameter("term", model.filtro);
            request.AddOrUpdateParameter("idVendedor", model.idVendedor);
            request.AddOrUpdateParameter("idCliente", model.idPersona);
            request.AddOrUpdateParameter("startDate", model.getFechaInicio());
            request.AddOrUpdateParameter("endDate", model.getFechaFin());
            request.AddOrUpdateParameter("transactionType", model.tipoTransaccion);
            request.AddOrUpdateParameter("transaccionPadre", model.transaccionPadre);
            request.AddOrUpdateParameter("status", model.status);

            var restResponse = DefaultClient.Execute<List<OutputDto>>(request);
            if (restResponse.IsSuccessful)
            {
                notasPedido = restResponse.Data;
            }

            // No devuelve pedidos si hubo un error
            return notasPedido;
        }

        public static async Task<HttpResponseMessage> DownloadOutputAsync(string id, string format = "PDF", string view = default)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("app_token", AppToken);

            return await client.GetAsync($"{Tools.GetApiUrlBase()}/inventory/{id}/download?format={format}&view={view}");
        }

        public static async Task<HttpResponseMessage> DownloadInventoryAsync(ProductoViewModel model, string format = "PDF")
        {

            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("app_token", AppToken);

            return await client.PostAsync($"{Tools.GetApiUrlBase()}/InventarioResumido/download?format={format}", data);            
        }

        public static async Task<HttpResponseMessage> DownloadKardexAsync(ProductoViewModel model, string format = "PDF")
        {

            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("app_token", AppToken);

            return await client.PostAsync($"{Tools.GetApiUrlBase()}/InventarioKardex/download?format={format}", data);
        }

        public static async Task<HttpResponseMessage> DownloadStockbyLocalAsync(StockLocalViewModel model, string format = "PDF")
        {

            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("app_token", AppToken);

            return await client.PostAsync($"{Tools.GetApiUrlBase()}/stockPorBodegas/download?format={format}", data);
        }

        public static async Task<HttpResponseMessage> DownloadCxcAsync(EntidadViewModel model, string format = "PDF")
        {

            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("app_token", AppToken);

            return await client.PostAsync($"{Tools.GetApiUrlBase()}/estadoDeCuentaClientes/download?format={format}", data);
        }

        public static async Task<HttpResponseMessage> DownloadMovimientosInventarioAsync(ProductViewModel model, string format = "PDF")
        {

            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("app_token", AppToken);

            return await client.PostAsync($"{Tools.GetApiUrlBase()}/consultaMovimientos/download?format={format}", data);
        }

        public static async Task<HttpResponseMessage> DownloadFacturasAsync(FacturaViewModels model, string format = "PDF")
        {

            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("app_token", AppToken);

            return await client.PostAsync($"{Tools.GetApiUrlBase()}/consultaFactura/download?format={format}", data);
        }

        public static async Task<HttpResponseMessage> DownloadVentasAsync(FacturaViewModels model, string format = "PDF")
        {

            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("app_token", AppToken);

            return await client.PostAsync($"{Tools.GetApiUrlBase()}/consultaVentas/download?format={format}", data);
        }

        public static async Task<HttpResponseMessage> DownloadEntradasAsync(FacturaViewModels model, string format = "PDF")
        {

            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("app_token", AppToken);

            return await client.PostAsync($"{Tools.GetApiUrlBase()}/consultaEntrada/download?format={format}", data);
        }

        public static MermaViewModel SaveMerma(MermaViewModel merma)
        {
            var request = new RestRequest("/warehouses/Inventory", Method.POST);
            request.AddJsonBody(merma);
            var restResponse = DefaultClient.Execute<MermaViewModel>(request);

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


        public static  TransferenciaViewModel CargarProductos(int idLocal, HttpPostedFileBase files)
        {
            var product = new ResultDto<TransferenciaViewModel>();
            var request = new RestRequest($"/products/cargar/{idLocal}", Method.POST);

            byte[] fileBytes;
            using (var binaryReader = new BinaryReader(files.InputStream))
            {
                fileBytes = binaryReader.ReadBytes(files.ContentLength);
            }

            request.AddFileBytes("archivo", fileBytes, files.FileName);

            var restResponse = OutputClient.Execute<ResultDto<TransferenciaViewModel>>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                //product = restResponse.Data;
                product = JsonConvert.DeserializeObject<ResultDto<TransferenciaViewModel>>(restResponse.Content);
                return  product.Result;
            }

            //var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse.Content);
            throw new ApplicationServicesException(product.Error);
        }

    }
}