using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using TendaGo.Common;
using TendaGo.Web.Infraestructura;
using RestSharp;
using TendaGo.Web.Models;
using System.Web.UI.WebControls;

namespace TendaGo.Web.ApplicationServices
{
    public class TipoUnidadAppService : AppServiceBase
    {
        public static List<UnitTypeDto> ObtenerTiposUnidadPorProducto(int idproducto, UnitTypeStatusEnum? idEstado = UnitTypeStatusEnum.Active)
        {
            var units = new List<UnitTypeDto>();
            var request = new RestRequest($"/products/{idproducto}/unit-types?idEstado={(short)idEstado}", Method.GET);
            var restResponse = DefaultClient.Execute<List<UnitTypeDto>>(request);
            
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                units = restResponse.Data;
                return units;
            }

            var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse.Content);
            throw new ApplicationServicesException(errorResponse);
        }


        public static UnitTypeDto GetUnitTypeById(int id)
        {
            UnitTypeDto unitType = null;

            var request = new RestRequest($"/unitTypes/{id}", Method.GET);

            var restResponse = DefaultClient.Execute<UnitTypeDto>(request);
            if (restResponse.IsSuccessful)
            {
                unitType = restResponse.Data;
            }
            return unitType;
        }

        public static List<UnitTypeDto> ObtenerTiposUnidad(int idproducto)
        {
            var units = new List<UnitTypeDto>();
            var request = new RestRequest($"/unitTypes?idProduct={idproducto}", Method.GET);
            var restResponse = DefaultClient.Execute<List<UnitTypeDto>>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                units = restResponse.Data;
                return units;
            }

            var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse.Content);
            throw new ApplicationServicesException(errorResponse);
        }

        public static UnitTypeDto ObtenerTipoUnidad(int idproducto, string unidad)
        {
            var request = new RestRequest($"/products/{idproducto}/unitTypes;name={unidad}", Method.GET);
            var restResponse = DefaultClient.Execute<UnitTypeDto>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                var units = restResponse.Data;
                return units;
            }

            var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse.Content);
            throw new ApplicationServicesException(errorResponse);
        }

        public static UnitTypeDto GuardarTipoUnidad(UnitTypeDto unitType)
        {
            var request = new RestRequest("/unitTypes", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddJsonBody(unitType);
            var restResponse = DefaultClient.Execute<UnitTypeDto>(request);
            if (restResponse.IsSuccessful)
            {
                var unitTypeSaved = restResponse.Data;
                return unitTypeSaved;
            }

            var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse.Content);
            throw new ApplicationServicesException(errorResponse);
        }
    }
}