using ER.BA;
using ER.BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http;
using TendaGo.Common;

namespace TendaGo.Api.Controllers
{

    // --Ronald Ramirez 2019-08-21
    /// <summary>
    /// Administra los recibos de las cuentas por cobrar
    /// </summary>
    [TokenAuthorize]
    public class receiptsController : ApiControllerBase
    {
        /// <summary>
        /// Obtener Recibos de cuentas por cobrar por usuario
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<ReceiptDto> GetReceipts(string search = null, int? idCustomer = null, DateTime? from = null, DateTime? end = null, int idLocal = 0, bool lsearch = false)
        {
            try
            {
                var parametros = new CobroDebitoFindParameterEntity
                {
                    IdEstado = 1,
                    Id = search,
                    Detalle = search,
                    IdEmpresa = CurrentUser.IdEmpresa
                };

                if (idCustomer != null)
                {
                    parametros.IdCliente = idCustomer.Value;
                }

                if (!lsearch)
                {
                    var result = CobroDebitoCollectionBussinesAction.FindByAll(parametros).Where(x => x.IdEstado == 1);
                    return result.Select(model => model.ToReceiptDto()).ToList();
                }
                else
                {
                    var result = CobroDebitoCollectionBussinesAction.search(parametros, from, end, idLocal);
                    return result.Select(model => model.ToReceiptDto()).ToList();
                }
            }
            catch (Exception ex)
            {
                string UserError = "Ocurrio un error general, reintente";
                if (ex.GetMessage().Contains("UQ_Recibo"))
                {
                    UserError = "No puede obtener los recibos";
                }

                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", UserError));
            }
        }

        /// <summary>
        /// Realiza el proceso de anulacion de un recibo.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public bool DeleteReceipts(string id)
        {
            try
            {
                var result = CobroDebitoBussinesAction.LoadByPK(id);

                if (result != null)
                {
                    result.SetDeletedState();
                    CobroDebitoBussinesAction.Save(result);
                }
                else
                {
                    throw new Exception($"No se encontro el recibo # {id}");
                }

            }
            catch (Exception ex)
            {
                string UserError = "Ocurrio un error general, reintente";
                if (ex.GetMessage().Contains("UQ_Recibo"))
                {
                    UserError = "No puede eliminar el recibo";
                }
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", UserError));
            }

            return false;
        }


        /// <summary>
        /// Realiza el proceso de anulacion de un recibo.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("receipts/{id}")]
        public ReceiptDto FindReceiptsById(string id)
        {
            try
            {
                var result = CobroDebitoBussinesAction.LoadByPK(id);
                if (result != null)
                {
                    var cobro = DetalleCobroCollectionBussinesAction.FindByAll(new DetalleCobroFindParameterEntity { IdCobroDebito = result.Id });
                    var medioCobro = DetalleMedioCobroCollectionBussinesAction.FindByAll(new DetalleMedioCobroFindParameterEntity { IdCobroDebito = result.Id });
                    var valor = cobro.Sum(x => x.Valor);

                    result.DetalleCobroFromIdCobroDebito = cobro;
                    result.DetalleMedioCobroFromIdCobroDebito = medioCobro;
                    result.Valor = valor;

                    return result.ToReceiptDto();
                }

                throw new Exception($"No se encontro el recibo # {id}");
            }
            catch (Exception ex)
            {
                string UserError = "Ocurrio un error general, reintente";
                if (ex.GetMessage().Contains("UQ_Recibo"))
                {
                    UserError = "No puede eliminar el recibo";
                }
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", UserError));
            }

        }



        /// <summary>
        /// Guarda el recibo de las cuentas por cobrar
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ReceiptDto PostReceipt(ReceiptDto model)
        {
            try
            {
                CobroDebitoEntity cobroDebito = model.ToCobroDebitoEntity();
                cobroDebito.UsuarioIngreso = model.CreatedBy;
                cobroDebito.IpIngreso = model.IpCreator;
                cobroDebito.FechaIngreso = DateTime.Now;
                cobroDebito.IdEmpresa = CurrentUser.IdEmpresa;

                cobroDebito = CobroDebitoBussinesAction.Save(cobroDebito);

                return cobroDebito.ToReceiptDto();
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

        /// <summary>
        /// Guarda el recibo de las cuentas por cobrar
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [HttpGet, Route("receipts/reverseReceipt")]
        public ReceiptDto ReverseReceipt(string id)
        {
            try
            {
                var result = CobroDebitoBussinesAction.LoadByPK(id);
                CobroDebitoEntity cob = new CobroDebitoEntity();

                if (result != null)
                {
                    result.UsuarioIngreso = CurrentUser.InicioSesion;
                    result.IpIngreso = result.IpIngreso;
                    result.FechaIngreso = DateTime.Now;
                    result.IdEmpresa = CurrentUser.IdEmpresa;
                    cob = CobroDebitoBussinesAction.Reverse(result);
                }
                var aaa = cob.ToReceiptDto();
                return cob.ToReceiptDto();
            }
            catch (Exception ex)
            {
                string UserError = "Ocurrio un error general, reintente";
                if (ex.GetMessage().Contains("UQ_Recibo"))
                {
                    UserError = "No puede eliminar el recibo";
                }
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", UserError));
            }
        }

        /// <summary>
        /// Actualiza el recibo de las cuentas por cobrar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public ReceiptDto PutReceipt(string id, ReceiptDto model)
        {
            try
            {
                CobroDebitoEntity cobroDebito = CobroDebitoBussinesAction.LoadByPK(id);
                cobroDebito.Detalle = model.Notes ?? "";
                cobroDebito.Fecha = model.IssuedOn;
                cobroDebito.IdEstado = model.IdStatus;
                cobroDebito.IdCliente = model.IdCustomer;

                // Cargamos los detalles
                if (cobroDebito.DetalleCobroFromIdCobroDebito == null)
                {
                    cobroDebito.DetalleCobroFromIdCobroDebito = new DetalleCobroEntityCollection();
                }
                else
                {
                    var deleteDetails = cobroDebito.DetalleCobroFromIdCobroDebito.Where(m =>
                    {
                        return model.Details.FirstOrDefault(x => x.Id == m.Id) == null;
                    });

                    foreach (var item in deleteDetails)
                    {
                        item.SetDeletedState();
                    }
                }

                cobroDebito.DetalleCobroFromIdCobroDebito.AddRange(model.Details.Select(m => m.ToDetalleCobroEntity()));

                if (cobroDebito.CurrentState.Equals(EntityStatesEnum.Updated))
                {
                    cobroDebito.UsuarioModificacion = model.ModifiedBy;
                    cobroDebito.IpModificacion = model.IpModifier;
                    cobroDebito.FechaModificacion = DateTime.Today;
                }


                cobroDebito = CobroDebitoBussinesAction.Save(cobroDebito);

                return cobroDebito.ToReceiptDto();
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

        [HttpGet, Route("receipts/summary")]
        public List<ReceiptSummaryDto> ReceiptsSummary(int? idLocal = null, DateTime? desde = null, DateTime? hasta = null, string vendedor = null)
        {
            DataSet pagos = CierreCajaBussinesAction.Consulta(CurrentUser.IdEmpresa, idLocal, desde, hasta, vendedor);

            var result = pagos.ConvertirToDto<ReceiptSummaryDto>().ToList();

            return result;
        }


    }
}
