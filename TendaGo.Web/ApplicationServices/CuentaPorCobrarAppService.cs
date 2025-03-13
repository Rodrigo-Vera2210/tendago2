using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;
using TendaGo.Common;
using TendaGo.Web.Infraestructura;

namespace TendaGo.Web.ApplicationServices
{
    public class CuentasPorCobrarAppService : AppServiceBase
    {
        public static ReceivableDto CuentaPorCobrarPorId(int idCuenta)
        {
            var receivable = new ReceivableDto();
            var request = new RestRequest($"/receivables/{idCuenta}", Method.GET);
            IRestResponse<ReceivableDto> restResponse = DefaultClient.Execute<ReceivableDto>(request);
            if (restResponse.IsSuccessful)
            {
                receivable = restResponse.Data;
            }
            return receivable;
        }

        public static List<ReceivableDto> CuentaPorCobrarPorSalidaId(string outputId)
        {
            var receivable = new List<ReceivableDto>();
            var request = new RestRequest($"output/{outputId}/receivables", Method.GET);

            IRestResponse<List<ReceivableDto>> restResponse = DefaultClient.Execute<List<ReceivableDto>>(request);

            if (restResponse.IsSuccessful)
            {
                receivable = restResponse.Data;
            }

            return receivable;
        }


    }
}