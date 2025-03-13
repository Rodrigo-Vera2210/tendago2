using ER.BA;
using ER.BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Http;
using TendaGo.Common;

namespace TendaGo.Api.Controllers
{
    [TokenAuthorize]
    public class inputController : ApiControllerBase
    {
        [HttpGet]
        [Route("inputs/validate-product")]
        public ValidationInputResultModel ValidateProduct(string productCode, string unitType, string local)
        {
            try
            {
                var validationResult = EntradaCollectionBussinesAction.ValidarCompra(productCode, unitType, local, CurrentUser.IdEmpresa);
                var dataRow = validationResult.Tables[0].Rows[0];
                var obs = new StringBuilder();

                int idProduct = int.Parse(dataRow[0].ToString());
                string productName = dataRow[1]?.ToString();
                int idTipoUnidad = int.Parse(dataRow[2].ToString());
                string unitTypeName = dataRow[3]?.ToString();
                int idLocal = int.Parse(dataRow[4].ToString());
                string localName = dataRow[5]?.ToString();

                bool existProduct = idProduct > 0;
                if (!existProduct)
                    obs.Append(productName).AppendLine();


                bool existUnitType = idTipoUnidad > 0;
                if (!existUnitType)
                    obs.Append(unitTypeName).AppendLine();

                bool existLocal = idLocal > 0;
                if (!existLocal)
                    obs.Append(localName).AppendLine();

                var result = new ValidationInputResultModel
                {
                    IsValid = existLocal && existProduct && existUnitType,
                    IdUnitType = idTipoUnidad,
                    UnitType = unitTypeName,
                    IdProduct = idProduct,
                    ProductName = productName,
                    IdLocal = idLocal,
                    Local = localName,
                    Observations = obs.ToString()
                };

                return result;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", "Ocurrio un error general, reintente"));
            }
        }


        /// <summary>
        /// Crea una entrada de inventario
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost, Route("input/create")]
        public InputDto PostInput(InputDto input)
        {
            try
            {
                EntradaEntity entradaEntity = input.ToEntradaEntity();

                entradaEntity.IdEmpresa = CurrentUser.IdEmpresa;

                SetPagoCredito(entradaEntity, input.CuentaPorPagarFromIdEntrada);

                if (string.IsNullOrEmpty(entradaEntity.Id))
                {
                    entradaEntity.SetNewState();
                    entradaEntity.TipoTransaccion = string.IsNullOrEmpty(entradaEntity.TipoTransaccion) ? "CP" : input.TipoTransaccion;
                    entradaEntity.Fecha = DateTime.Now;
                    entradaEntity.Periodo = entradaEntity.Fecha.ToString("yyyyMM");
                    entradaEntity.EstadoActual = string.IsNullOrEmpty(entradaEntity.EstadoActual) ? "EN PROCESO" : input.EstadoActual;
                    entradaEntity.FechaIngreso = DateTime.Now;
                    entradaEntity.IdEstado = 1;
                    entradaEntity.Total = entradaEntity.DetalleEntradaFromIdEntrada.Sum(x => x.Subtotal);
                    entradaEntity.Saldo = entradaEntity.Total;
                }
                else
                {
                    entradaEntity.SetUpdatedState();
                }

                var newSalidaEntity = EntradaBussinesAction.Guardar(entradaEntity, null, null, null);
                return newSalidaEntity.ToInputDto();
            }
            catch (SqlException ex)
            {
                throw Request.BuildHttpErrorException(HttpStatusCode.InternalServerError, ex.ToString(), ex.GetMessage());
            }
            catch (HttpResponseException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                string UserError = "Ocurrio un error general, reintente";
                if (ex.GetMessage().Contains("PK_Entrada"))
                {
                    UserError = "No puede ingresar una Entrada duplicada";
                }
                throw Request.BuildHttpErrorException(HttpStatusCode.BadRequest,
                    $"{ex.GetAllMessages()}", UserError);
            }
        }


        [HttpGet, Route("input/{id}")]
        public InputDto GetInput(string id)
        {
            try
            {
                EntradaEntity entrada = GetEntradaEntity(id);

                if (entrada == null)
                {
                    throw new Exception($"La compra o pedido # {id} no existe!");
                }

                var input = entrada.ToInputDto();

                foreach (var item in input.DetalleEntradaFromIdEntrada)
                {
                    var product = ProductoBussinesAction.LoadByPK(item.IdProducto);
                    var tipoUnidad = TipoUnidadBussinesAction.LoadByPK(item.IdTipoUnidad);
                    var local = LocalBodegaBussinesAction.LoadByPK(item.IdLocal);

                    item.NombreProducto = $"{product.Producto} - {product.IdMarcaAsMarca?.Marca}";
                    item.CobraIva = product.CobraIva;
                    item.CodigoInterno = product.CodigoInterno;
                    item.DesTipoUnidad = tipoUnidad.TipoUnidad;
                    item.UnidadMedida = tipoUnidad.UnidadMedidadAsUnidadMedida?.UnidadMedida ?? item.DesTipoUnidad;
                    item.Local = local.Local;
                }

                if (!string.IsNullOrEmpty(input.TransaccionPadre))
                {
                    input.Salida = SalidaCollectionBussinesAction
                        .FindByAll(new SalidaFindParameterEntity { TransaccionPadre = input.TransaccionPadre })?
                        .FirstOrDefault()?.ToOutputDto();
                }

                return input;
            }
            catch (HttpResponseException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", "Ocurrio un error general, reintente"));
            }
        }

        /// <summary>
        /// Elimina una salida de inventario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost, Route("input/{id}/delete")]
        public IHttpActionResult DeleteInput(string id, InputDeleteDto dto)
        {
            try
            {
                var entradaEntity = GetEntradaEntity(id);

                if (entradaEntity == null)
                {
                    throw new Exception("PK_Entrada");
                }

                //if (!string.IsNullOrEmpty(entradaEntity.TransaccionPadre))
                //{
                //    var salida = SalidaCollectionBussinesAction
                //                            .FindByAll(new SalidaFindParameterEntity { TransaccionPadre = entradaEntity.TransaccionPadre })?
                //                            .FirstOrDefault(x => x.TransaccionPadre == entradaEntity.TransaccionPadre);

                //    if (salida != null)
                //    {
                //        return new outputController().DeleteOutput(salida.Id, new OutputDeleteDto
                //        {
                //            FechaIngreso = dto.FechaIngreso,
                //            UsuarioIngreso = dto.UsuarioIngreso,
                //            IpIngreso = dto.IpIngreso,
                //            IdSalida = salida.Id,
                //        });
                //    }
                //}

                entradaEntity.SetDeletedState();
                entradaEntity.EstadoActual = "ANULADA";
                entradaEntity.FechaModificacion = dto.FechaIngreso;
                entradaEntity.UsuarioModificacion = dto.UsuarioIngreso;
                entradaEntity.IpModificacion = dto.IpIngreso;
                entradaEntity.IdEstado = 0;

                var result = EntradaBussinesAction.Guardar(entradaEntity);

                if (result.CurrentState == EntityStatesEnum.SavedSuccessfully)
                {
                    return Ok(true);
                }

                throw new Exception("Hubo un error al eliminar el documento");
            }
            catch (SqlException ex)
            {
                string UserError = "Ocurrio un error general, reintente";

                if (ex.Procedure.Contains("Custom_Stock_CostoPromedioPonderado"))
                {
                    UserError = ex.GetMessage();
                }
                else if (ex.GetMessage().Contains("PK_Entrada"))
                {
                    UserError = "No existe la salida especificada";
                }

                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
                    $"{ex.GetAllMessages()}", UserError));
            }
            catch (Exception ex)
            {
                string UserError = "Ocurrio un error general, reintente";

                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
                    $"{ex.GetAllMessages()}", UserError));
            }
        }


    }
}
