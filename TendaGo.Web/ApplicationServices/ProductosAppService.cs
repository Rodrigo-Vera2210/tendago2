using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using RestSharp;
using TendaGo.Common;
using TendaGo.Web.Infraestructura;
using Newtonsoft.Json;
using TendaGo.Web.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using TendaGo.Common.EntitiesDto;
using AutoMapper;

namespace TendaGo.Web.ApplicationServices
{
    public class ProductosAppService : AppServiceBase
    {

        public static List<LiteProductDto> SearchProductsByTerm(string searchTerm = "")
        {
            var request = new RestRequest($"/products/find?searchTerm={searchTerm}", Method.GET);
            var restResponse = DefaultClient.Execute<List<LiteProductDto>>(request);
            if (restResponse.IsSuccessful)
            {
                return restResponse.Data;
            }
            return new List<LiteProductDto>();
        }
         
        public static LiteProductDto GetProductByInternalCode(string internalCode)
        {
            var request = new RestRequest($"/products/internal-code?term={internalCode}", Method.GET);
            var restResponse = DefaultClient.Execute<LiteProductDto>(request);
            if (restResponse.IsSuccessful)
            {
                return restResponse.Data;
            }
            return null;
        }
        public static LiteProductDto GetProductByProviderCode(string internalCode)
        {
            var request = new RestRequest($"/products/provider-code?term={internalCode}", Method.GET);
            var restResponse = DefaultClient.Execute<LiteProductDto>(request);
            if (restResponse.IsSuccessful)
            {
                return restResponse.Data;
            }
            return null;
        }

        public static List<LiteStockDto> GetProductsStocks(int idLocal, string productList)
        {
            List<LiteStockDto> productStock = new List<LiteStockDto>();
            var request = new RestRequest($"products/stocks;local={idLocal}", Method.POST);
            request.AddObject(productList);
            var restResponse = DefaultClient.Execute<List<LiteStockDto>>(request);

            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                productStock = restResponse.Data;
            }

            return productStock;
        }


        public static List<TarifaImpuestoDto> GetImpuestosVigentes()
        {
            ResultList<List<TarifaImpuestoDto>> impuestos = new ResultList<List<TarifaImpuestoDto>>();
            var request = new RestRequest($"products/tarifa-impuesto", Method.GET);
            var restResponse = OutputClient.Execute<ResultList<List<TarifaImpuestoDto>>>(request);

            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                //impuestos = Mapper.Map<List<TarifaImpuestoDto>>(restResponse.Data.Result);
                impuestos = JsonConvert.DeserializeObject<ResultList<List<TarifaImpuestoDto>>>(restResponse.Content);

            }

            return impuestos.Result;
        }

        public static List<LiteStockDto> ObtenerStockProductoLocales(int idProducto)
        {
            List<LiteStockDto> productStock = new List<LiteStockDto>();
            var request = new RestRequest($"/products/{idProducto}/stock", Method.GET);
            var restResponse = DefaultClient.Execute<List<LiteStockDto>>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                productStock = restResponse.Data;
            }

            return productStock;
        }

        public static ProductDto ObtenerStockProducto(int idProducto, int idLocal)
        {
            ProductDto productStock = null;
            var request = new RestRequest($"/products/{idProducto}/stock;local={idLocal}", Method.GET);
            var restResponse = DefaultClient.Execute<ProductDto>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                productStock = restResponse.Data;
            }

            return productStock;
        }

        public static ProductDto ObtenerProducto(int id)
        {
            ResultDto<ProductDto> product = null;
            var request = new RestRequest($"/products/{id}", Method.GET);
            //var restResponse = DefaultClient.Execute<ProductDto>(request);
            var restResponse = OutputClient.Execute<ResultDto<ProductDto>>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                //product = restResponse.Data;
                product = JsonConvert.DeserializeObject<ResultDto<ProductDto>>(restResponse.Content);
                return product.Result;
            }
            
            var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse.Data.Message);
            throw new ApplicationServicesException(errorResponse);
        }

        public static List<ProductDto> BuscarProductos(SearchProductModel busquedaModel)
        {
            var products = new List<ProductDto>();
            var request = new RestRequest("/products/search", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(busquedaModel);
            request.Timeout = Convert.ToInt32(TimeSpan.FromMinutes(5).TotalMilliseconds);
            var restResponse = DefaultClient.Execute<List<ProductDto>>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                products = restResponse.Data;
                return products;
            }

            if (!string.IsNullOrEmpty(restResponse?.Content))
            {
                var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse?.Content);
                if (string.IsNullOrEmpty(errorResponse?.TechnicalMessage))
                {
                    var genericApiResponse = JsonConvert.DeserializeObject<GeneriApiErrorResponse>(restResponse?.Content);
                    throw new ApplicationServicesException(genericApiResponse?.Message);
                }
            } 
            
            throw new ApplicationServicesException(restResponse?.ErrorMessage);
        }

        public static ProductDto GuardarProducto(ProductDto productDto)
        {
            //var request = new RestRequest("/products", Method.POST);
            var request = new RestRequest("/products/save", Method.POST);

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(productDto);

            //var restResponse = DefaultClient.Execute<ProductDto>(request);
            var restResponse = OutputClient.Execute<ResultDto<ProductDto>>(request);
            
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                //var product = JsonConvert.DeserializeObject<ProductDto>(restResponse.Content);
                //return product;
                return restResponse.Data.Result;
            }
            else
            {
                var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse.Content);
                if (!string.IsNullOrEmpty(errorResponse?.TechnicalMessage))
                {
                    throw new ApplicationServicesException(errorResponse);
                }

                var genericApiResponse = JsonConvert.DeserializeObject<GeneriApiErrorResponse>(restResponse.Content);

                if (!string.IsNullOrEmpty(genericApiResponse?.Message))
                {
                    throw new ApplicationServicesException(genericApiResponse.Message);
                }

                throw new Exception("Hubo un error al Guardar el producto!");
            }
        }

        public static void DeleteProduct(ProductDto producto)
        {
            var request = new RestRequest("/products", Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(producto);
            var restResponse = DefaultClient.Delete(request);

            if (!restResponse.StatusCode.Equals(HttpStatusCode.OK))
            { 
                var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse.Content);
                if (!string.IsNullOrEmpty(errorResponse?.TechnicalMessage))
                {
                    throw new ApplicationServicesException(errorResponse);
                }

                var genericApiResponse = JsonConvert.DeserializeObject<GeneriApiErrorResponse>(restResponse.Content);

                if (!string.IsNullOrEmpty(genericApiResponse?.Message))
                {
                    throw new ApplicationServicesException(genericApiResponse.Message);
                }

                throw new Exception("Hubo un error al Eliminar el producto!");
            }
        }

        public static async Task<HttpResponseMessage> DownloadProductosAsync(FacturaViewModels model, string format = "PDF")
        {

            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("app_token", AppToken);

            return await client.PostAsync($"{Tools.GetApiUrlBase()}/consultaProductos/download?format={format}", data);
        }
    }
}