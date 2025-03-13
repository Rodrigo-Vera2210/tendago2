using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using RestSharp;
using TendaGo.Common;
using TendaGo.Web.Infraestructura;
using TendaGo.Common.EntitiesDto;
using AutoMapper;
using TendaGo.Web.Models;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using TendaGo.Common.RequestModels;

namespace TendaGo.Web.ApplicationServices
{
    public class MedioPagoAppService : AppServiceBase
    {
        public static List<PaymentMethodDto> ObtenerMediosDePago()
        {
            var mediosPago = new List<PaymentMethodDto>();
            var request = new RestRequest("/paymentMethods", Method.GET);
            var restResponse = DefaultClient.Execute<List<PaymentMethodDto>>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                mediosPago = restResponse.Data;
            }
            return mediosPago;
        }




        //internal static FormaPagoSRIDto ToFormaPagoSRIDto(this DocumentDetailDto dto)
        //{
        //    var conf = new MapperConfiguration(config => config.CreateMap<DocumentDetailDto, DetalleDocumentoEntity>());
        //    var mapper = conf.CreateMapper();
        //    var entity = mapper.Map<DetalleDocumentoEntity>(dto);
        //    return entity;
        //}


        public static List<FormaPagoSRIDto> ObtenerFormasPagoSri()
        {
            var request = new RestRequest($"/forma/pagos?page=1", Method.GET);
            request.Timeout = (int)TimeSpan.FromMinutes(5).TotalMilliseconds;
            var restResponse = FacturaGOClient.Execute<ResultList<List<FormaPagoSRIDto>>>(request);

            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                List<FormaPagoSRIDto> formaPagoSri = restResponse.Data.ToFormaPagoSRIDto();
                return formaPagoSri;
            }
            return default;
        }

        public static SalidaFormaPagoSriDto ObtenerFormasPagoSriBySalida(string idSalida)
        {
            var request = new RestRequest($"/formapago/{idSalida}", Method.GET);
            request.Timeout = (int)TimeSpan.FromMinutes(5).TotalMilliseconds;
            var restResponse = OutputClient.Execute<ResultDto<SalidaFormaPagoSriDto>>(request);

            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                var salidaFormaPagoSri = JsonConvert.DeserializeObject<ResultDto<SalidaFormaPagoSriDto>>(restResponse.Content);
                return salidaFormaPagoSri.Result;
            }
            return default;
        }

        public static SalidaFormaPagoSriDto ActualizarPagoSriBySalida(SalidaFormaPagoSriRequest model)
        {
            //var requestModel = ToFormaPagoSRIDto(model);
            var request = new RestRequest($"/formapago", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(model);
            request.Timeout = (int)TimeSpan.FromMinutes(5).TotalMilliseconds;
            var restResponse = OutputClient.Execute<ResultDto<SalidaFormaPagoSriDto>>(request);

            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                var salidaFormaPagoSri = JsonConvert.DeserializeObject<ResultDto<SalidaFormaPagoSriDto>>(restResponse.Content);
                return salidaFormaPagoSri.Result;
            }
            return default;
        }

    }
}