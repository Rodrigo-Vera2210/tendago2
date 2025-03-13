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
    public class UnidadMedidaAppService : AppServiceBase
    {
        public static List<MeasurementUnitDto> ObtenerUnidadesDeMedida()
        {
            var units = new List<MeasurementUnitDto>();
            var request = new RestRequest("/measurementUnits/all", Method.GET);

            var restResponse = DefaultClient.Execute<List<MeasurementUnitDto>>(request);
            if (restResponse.IsSuccessful)
            {
                units = restResponse.Data;
            }
            return units;
        }

        public static List<MeasurementUnitDto> ObtenerUnidadesDeMedidaActivas()
        {
            var units = new List<MeasurementUnitDto>();
            var request = new RestRequest("/measurementUnits", Method.GET);

            var restResponse = DefaultClient.Execute<List<MeasurementUnitDto>>(request);
            if (restResponse.IsSuccessful)
            {
                units = restResponse.Data;
            }
            return units;
        }

        internal static MeasurementUnitDto ObtenerUnidad(int id)
        {
            var unit = new MeasurementUnitDto();
            var request = new RestRequest($"/measurementUnits/{id}", Method.GET);

            var restResponse = DefaultClient.Execute<MeasurementUnitDto>(request);
            if (restResponse.IsSuccessful)
            {
                unit = restResponse.Data;
            }
            return unit;
        }

        internal static LineDto GuardarUnidad(MeasurementUnitDto unit)
        {
            var request = new RestRequest("/measurementUnits", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddJsonBody(unit);
            var restResponse = DefaultClient.Execute<LineDto>(request);
            if (restResponse.IsSuccessful)
            {
                var unitSaved = restResponse.Data;
                return unitSaved;
            }

            var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse.Content);
            throw new ApplicationServicesException(errorResponse);
        }
    }
}