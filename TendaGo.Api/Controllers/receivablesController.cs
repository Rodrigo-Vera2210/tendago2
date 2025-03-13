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
    // Administra las cuentas por cobrar
    // --Ronald Ramirez 2019-08-21
    [TokenAuthorize]
    public class receivablesController : ApiControllerBase
    {

        // Metodo para :
        // GET clients/{idClient}/receivables
        /// <summary>
        /// Devuelve las cuentas por cobrar pendientes de un cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("clients/{id}/receivables")]
        public List<ReceivableDto> GetReceivablesByCustomer(int id)
        {
            var cuentasCobrar = CuentaPorCobrarCollectionBussinesAction.FindByIdCliente(CurrentUser.IdEmpresa, id);
            var cuotas = cuentasCobrar
                        .Select(m => m.ToReceivableDto())
                        .ToList();

            return cuotas;
        }

        [HttpGet, Route("receivables/{id}")]
        public ReceivableDto GetReceivableById(int id)
        {
            var cuentasCobrar = CuentaPorCobrarBussinesAction.LoadByPK(id);
            return cuentasCobrar.ToReceivableDto();
        }

        [HttpGet, Route("output/{outputId}/receivables")]
        public List<ReceivableDto> GetReceivableByOutputTd(string outputId)
        {
            var cuentasCobrar = CuentaPorCobrarCollectionBussinesAction.FindByAll(new CuentaPorCobrarFindParameterEntity
            {
                IdSalida = outputId
            });

            var cuotas = cuentasCobrar
                        .Select(m => m.ToReceivableDto())
                        .OrderBy(p => p.Numero)
                        .ToList();

            if (cuotas.Count > 0)
            {
                foreach (var cxc in cuotas)
                {
                    cxc.MetodosPago = new List<ReceivablePayMethodDto>();
                    var metodos = DetalleCobroCollectionBussinesAction.FindByAll(new DetalleCobroFindParameterEntity { IdCuentaPorCobrar = cxc.Id, IdEstado = 1 });
                    foreach (var meth in metodos)
                    {
                        cxc.MetodosPago.Add(new ReceivablePayMethodDto() { IdMedioPago = meth.IdMedioPago, Valor = meth.Valor });

                    }
                }
            }

            return cuotas;
        }

        /// <summary>
        /// Actualiza cuentas por cobrar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("receivables/{outputId}/{outputIdNew}/receivables")]
        public IHttpActionResult PutReceivableByOutputTd(string outputId, string outputIdNew)
        {
            try
            {
                CuentaPorCobrarEntityCollection cuentasPorCobrar = CuentaPorCobrarCollectionBussinesAction.FindByIdSalida(outputId);

                foreach (CuentaPorCobrarEntity cxc in cuentasPorCobrar)
                {
                    cxc.IdSalida = outputIdNew;
                    cxc.UsuarioModificacion = User.Identity.Name;
                    cxc.IpModificacion = "1.1.1";
                    cxc.FechaModificacion = DateTime.Today;
                    cxc.CurrentState = EntityStatesEnum.Updated;

                    CuentaPorCobrarBussinesAction.Save(cxc);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                string UserError = "Ocurrio un error general, reintente";
                if (ex.GetMessage().Contains("UQ_Recibo"))
                {
                    UserError = "No puede generar un recibo duplicado";
                }
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", UserError));
            }
        }


    }
}
