using Newtonsoft.Json;
using RestSharp;
using TendaGo.Common;
using TendaGo.Web.Infraestructura;
using TendaGo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace TendaGo.Web.ApplicationServices
{
    public class CatalogosAppService : AppServiceBase
    {
        public static List<CurrencyDto> ObtenerMonedas()
        {
            var request = new RestRequest($"/catalogs/currencies", Method.GET);
            var restResponse = DefaultClient.Execute<List<CurrencyDto>>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                return restResponse.Data;
            }

            return new List<CurrencyDto>();
        }

        public static CurrencyDto ObtenerMoneda(short idMoneda)
        {
            var request = new RestRequest($"/catalogs/currencie/{idMoneda}", Method.GET);
            var restResponse = DefaultClient.Execute<CurrencyDto>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                return restResponse.Data;
            }

            return new CurrencyDto();
        }

        public static List<CountryDto> ObtenerPaises()
        {
            var countries = new List<CountryDto>();
            var request = new RestRequest($"/countries", Method.GET);
            var restResponse = DefaultClient.Execute<List<CountryDto>>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                countries = restResponse.Data;
                return countries;
            }
            var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse.Content);
            throw new ApplicationServicesException(errorResponse);
        }

        public static List<ProvinceDto> ObtenerProvincias(short idPais)
        {
            var provinces = new List<ProvinceDto>();            
            var request = new RestRequest($"/countries/{idPais}/Provincias", Method.GET);
            var restResponse = DefaultClient.Execute<List<ProvinceDto>>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                provinces = restResponse.Data;
                return provinces;
            }
            var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse.Content);
            throw new ApplicationServicesException(errorResponse);
        }

        public static List<CityDto> ObtenerCiudades(int idProvincia)
        {
            var cities = new List<CityDto>();
            var request = new RestRequest($"/provinces/{idProvincia}/Ciudades", Method.GET);
            var restResponse = DefaultClient.Execute<List<CityDto>>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                cities = restResponse.Data;
                return cities;
            }
            var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse.Content);
            throw new ApplicationServicesException(errorResponse);
        }
        public static List<SectorDto> ObtenerSectores()
        {
            var sectores = new List<SectorDto>();
            var request = new RestRequest($"/catalogs/sectors", Method.GET);
            var restResponse = DefaultClient.Execute<List<SectorDto>>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                sectores = restResponse.Data;
                return sectores;
            }
            var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse.Content);
            throw new ApplicationServicesException(errorResponse);
        }


        public static List<PaymentMethodDto> ObtenerMediosPago()
        {
            var methods = new List<PaymentMethodDto>();            
            var request = new RestRequest("/paymentMethods", Method.GET);            
            var restResponse = DefaultClient.Execute<List<PaymentMethodDto>>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                methods = restResponse.Data;
                return methods;
            }
            var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse.Content);
            throw new ApplicationServicesException(errorResponse);
        }


        public static List<SelectListItem> ObtenerFormasPago()
        {
            return new List<SelectListItem>(){
                new SelectListItem { Text = "CONTADO", Value = "1"},
                new SelectListItem { Text = "CREDITO", Value = "2"},
            };
        }

        public static List<PackageTypeDto> ObtenerTiposPaquete()
        {
            var result = new List<PackageTypeDto>();
            var request = new RestRequest("/packageTypes", Method.GET);
            var restResponse = DefaultClient.Execute<List<PackageTypeDto>>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                result = restResponse.Data;
                return result;
            }
            var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse.Content);
            throw new ApplicationServicesException(errorResponse);
        }
    }
}