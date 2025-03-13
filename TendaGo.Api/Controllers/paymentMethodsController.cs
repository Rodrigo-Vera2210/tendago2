using ER.BA;
using ER.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using TendaGo.Common;

namespace TendaGo.Api.Controllers
{
    [TokenAuthorize]
    public class paymentMethodsController : ApiControllerBase
    {
        public List<PaymentMethodDto> GetPaymentMethods()
        {
            var medioPagoFindParameter = new ER.BE.MedioPagoFindParameterEntity { IdEstado = 1 };
            var paymentMethods = ER.BA.MedioPagoCollectionBussinesAction.FindByAll(medioPagoFindParameter);
            var paymentMethodsDto = paymentMethods.Select(py => py.GlobalMapperConverter<ER.BE.MedioPagoEntity, PaymentMethodDto>()).ToList();
            return paymentMethodsDto;
        }

        [HttpGet, Route("getPaymentMethodsByPK/{id}")]
        public PaymentMethodDto GetPaymentMethodsByPK(int id)
        {
            return MedioPagoBussinesAction.LoadByPK(id).ToPaymentMethodDto();
        }

        [HttpPost]
        public PaymentMethodDto PostPaymentMethod(PaymentMethodDto payment)
        {
            var methodEntity = new MedioPagoEntity();

            if (payment.Id > 0)
            {
                methodEntity = MedioPagoBussinesAction.LoadByPK(payment.Id);
                methodEntity.IdEmpresa = CurrentUser.IdEmpresa;

                methodEntity.MedioPago = payment.MedioPago;
                methodEntity.FechaModificacion = DateTime.Today;
                methodEntity.IpModificacion = "::1";
                methodEntity.UsuarioModificacion = CurrentUser.InicioSesion;
            }
            else
            {
                methodEntity = payment.GlobalMapperConverter<PaymentMethodDto, MedioPagoEntity>();
                methodEntity.IdEmpresa = CurrentUser.IdEmpresa;
                methodEntity.MedioPago = payment.MedioPago;
                methodEntity.IpIngreso = "::1";
                methodEntity.UsuarioIngreso = CurrentUser.InicioSesion;
                methodEntity.FechaIngreso = DateTime.Today;
                methodEntity.IdEstado = 1;
                methodEntity.SetNewState();
            }

            methodEntity = MedioPagoBussinesAction.Save(methodEntity);
            return methodEntity.ToPaymentMethodDto();
        }

        [HttpDelete]
        public IHttpActionResult DeleteMethod(PaymentMethodDto payment)
        {
            try
            {
                var methodEntity = MedioPagoBussinesAction.LoadByPK(payment.Id);
                if (methodEntity == null)
                    throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, "Metodo de pago no existe", "El Pago solicitado, no existe"));

                methodEntity.UsuarioModificacion = CurrentUser.InicioSesion;
                methodEntity.IpModificacion = "::1";
                methodEntity.FechaModificacion = DateTime.Today;
                methodEntity.SetDeletedState();
                MedioPagoBussinesAction.Save(methodEntity);
                return Ok();
            }
            catch (HttpResponseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", "Ocurrio un error general, reintente"));
            }
        }

    }
}
