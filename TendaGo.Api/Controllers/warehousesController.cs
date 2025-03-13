using ER.BA;
using ER.BE;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Http;
using TendaGo.Common;

namespace TendaGo.Api.Controllers
{
    [TokenAuthorize]
    public class warehousesController : ApiControllerBase
    {

        public List<WarehouseDto> GetWarehouses()
        {
            var warehouses = LocalBodegaCollectionBussinesAction.FindByAll(new LocalBodegaFindParameterEntity { IdEmpresa = CurrentUser.IdEmpresa });
            var warehousesDtoList = warehouses.Select(w => w.ToWarehouseDto()).ToList();
            return warehousesDtoList;
        }

        public WarehouseDto GetWarehouse(string id)
        {
            int idConverted;
            bool isValidId = int.TryParse(id, out idConverted);
            if (!isValidId)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro id, es invalido", "Id invalido"));


            var warehouse = LocalBodegaCollectionBussinesAction.FindByAll(new LocalBodegaFindParameterEntity { IdEmpresa = CurrentUser.IdEmpresa, Id = idConverted });
            return warehouse.FirstOrDefault().ToWarehouseDto();
        }

        [HttpPost, Route("warehouses/inventory")]
        public AdjustInventoryDto WarehouseAdjustInventory(AdjustInventoryRequest model)
        {
            SqlConnection connection = null;
            SqlTransaction transaction = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]))
                {
                    connection.Open();
                    transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                    var result = model.ToAdjustInventoryDto();
                    result.Id = Guid.NewGuid().ToString();
                    result.TipoTransaccion = "AJUS";
                    result.IdEmpresa = CurrentUser.IdEmpresa;

                    var productos = result.Detalle
                                        .Select(x => x.IdProducto.ToString())
                                        .JoinString(",");

                    var stockActual = StockCollectionBussinesAction.StockInventario(result.IdEmpresa, productos, model.IdLocal, connection, transaction)
                                                                        .ConvertirToDto<StockDto>();

                    foreach (var item in stockActual)
                    {
                        var detalle = result.Detalle.Find(x => x.IdProducto == item.IdProducto);
                        if (detalle != null)
                        {
                            detalle.Precio = (decimal)(result.IdEmpresa != 1 ? (item.Costo != 0 ? item.Costo : 0M)
                                                                             : (item.Costo != 0 ? item.Costo : item.ValorUnitario));
                            detalle.CantidadIn0 = detalle.CantidadIn0;
                        }
                    }

                    var salida = result.GenerarSalidaAju();
                    var entrada = result.GenerarEntradaAju();

                    //foreach (var item in result.Detalle)
                    //{
                    //    var detalleE = new DetalleEntradaEntity();
                    //    var detalleS = new DetalleSalidaEntity();
                    //    bool aumento = false;
                    //    if (entrada != null)
                    //    {
                    //         detalleE = entrada.DetalleEntradaFromIdEntrada.FirstOrDefault(x => x.IdProducto == item.IdProducto);

                    //    }
                    //    if (salida != null)
                    //    {
                    //         detalleS = salida.DetalleSalidaFromIdSalida.FirstOrDefault(x => x.IdProducto == item.IdProducto);
                    //    }

                    //    if (detalleE != null && detalleS != null)
                    //    {
                    //        aumento = detalleE.Cantidad > detalleS.Cantidad;

                    //    }

                    //    //if (detalleS != null && detalleS.Cantidad > 0 && !aumento)
                    //    if (detalleS != null && detalleS.Cantidad > 0 && aumento)
                    //        {
                    //        detalleS.Estado = 0; // Cambia esto al nuevo estado que desees                            
                    //    }
                    //    else if(detalleE != null && (detalleS == null || detalleS.Cantidad == 0))
                    //    { 
                    //        detalleE.Estado = 0; // Cambia esto al nuevo estado que desees
                    //    }
                    //}

                    if (salida != null)
                        if (salida.DetalleSalidaFromIdSalida[0].Cantidad > 0)
                            result.Salida = SalidaBussinesAction.Guardar(salida, connection, transaction)?.ToOutputDto();

                    if (entrada != null)
                        result.Entrada = EntradaBussinesAction.Guardar(entrada, connection, transaction)?.ToInputDto();

                    transaction.Commit();
                    transaction = null;

                    return result;
                }
            }
            catch (HttpResponseException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                string UserError = "Ocurrio un error general, reintente";
                if (ex.GetMessage().Contains("PK_Salida"))
                {
                    UserError = "No puede generar una salida duplicada";
                }

                if (ex.GetMessage().Contains("PK_Entrada"))
                {
                    UserError = "No puede generar una entrada duplicada";
                }

                if (ex.GetMessage().Contains("no hay existencias"))
                {
                    UserError = "No hay existencias para el producto seleccionado";
                }

                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
                    $"{ex.GetAllMessages()}", UserError));
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Dispose();
                }
            }
        }



        [HttpPost, Route("warehouses/transfer")]
        public List<TransferDto> WarehouseTransfer(TransferRequest model)
        {
            SqlConnection connection = null;
            SqlTransaction transaction = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]))
                {
                    connection.Open();
                    transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                    model.IdEmpresa = model.IdEmpresa > 0 ? model.IdEmpresa : CurrentUser.IdEmpresa;

                    var results = Transferir(model, connection, transaction);

                    transaction.Commit();
                    transaction = null;

                    return results;
                }
            }
            catch (HttpResponseException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                string UserError = "Ocurrio un error general, reintente";
                if (ex.GetMessage().Contains("PK_Salida"))
                {
                    UserError = "No puede generar una salida duplicada";
                }

                if (ex.GetMessage().Contains("PK_Entrada"))
                {
                    UserError = "No puede generar una entrada duplicada";
                }

                if (ex.GetMessage().Contains("no hay existencias"))
                {
                    UserError = "No hay existencias para el producto seleccionado";
                }

                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
                    $"{ex.GetAllMessages()}", UserError));
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Dispose();
                }
            }
        }

        public static List<TransferDto> Transferir(TransferRequest model, SqlConnection connection, SqlTransaction transaction)
        {
            var locales = model.Detalle.Select(x => new { x.IdLocal }).Distinct().Select(x => x.IdLocal).ToList();
            var results = new List<TransferDto>();
            foreach (var idLocalOrigen in locales)
            {
                var bodegaDestino = LocalBodegaBussinesAction.LoadByPK(model.IdLocalDestino, connection, transaction);
                if (bodegaDestino == null)
                    throw new Exception("No ha especificado la bodega de destino");

                if (idLocalOrigen == model.IdLocalDestino)
                    throw new Exception("La bodega origen no puede ser igual al destino.");

                var bodegaDesde = LocalBodegaBussinesAction.LoadByPK(idLocalOrigen, connection, transaction);
                if (bodegaDesde == null)
                    throw new Exception("No ha especificado la bodega de origen");

                var result = model.ToTransferDto();
                result.IdLocalOrigen = result.IdLocalOrigen > 0 ? result.IdLocalOrigen : idLocalOrigen;
                result.Id = Guid.NewGuid().ToString();
                result.TipoTransaccion = "TRAN";

                result.Detalle = model.Detalle.Where(x => x.IdLocal == idLocalOrigen).ToList();

                var salida = result.GenerarSalida(idLocalOrigen);
                var entrada = result.GenerarEntrada(result.IdLocalDestino);

                result.Salida = SalidaBussinesAction.Guardar(salida, connection, transaction)?.ToOutputDto();
                result.Entrada = EntradaBussinesAction.Guardar(entrada, connection, transaction)?.ToInputDto();

                results.Add(result);
            }

            return results;

        }

        [HttpGet, Route("warehouses/{idLocal}/inputs/search")]
        public List<InputDto> SearchInputs(int idLocal, string term = null, string idVendedor = null, int? idProveedor = null, DateTime? startDate = null, DateTime? endDate = null, TransactionType? transactionType = null, TransactionStatus? status = null)
        {
            var entradas = EntradaCollectionBussinesAction.Search(CurrentUser.IdEmpresa, idLocal, transactionType.GetCode(), term, idVendedor, idProveedor, startDate, endDate, status.GetStatus());
            var outputs = entradas.Select(x => x.ToInputDto()).ToList();
            return outputs;
        }

        [HttpGet, Route("warehouses/{idLocal}/outputs/search")]
        public List<OutputDto> SearchOutputs(int idLocal = 0, string term = null, string idVendedor = null, int? idCliente = null, DateTime? startDate = null, DateTime? endDate = null, TransactionType? transactionType = null, string transaccionPadre = null, TransactionStatus? status = null)
        {
            var salidasEntity = SalidaCollectionBussinesAction.Search(CurrentUser.IdEmpresa, idLocal, transactionType.GetCode(), term, idVendedor, idCliente, startDate, endDate, status.GetStatus(), null, null, transaccionPadre);
            var outputs = salidasEntity.Select(x => x.ToOutputLiteDto()).ToList();
            return outputs;
        }


        [HttpGet, Route("warehouses/{idLocal}/outputs/{currenStatus?}")]
        public List<OutputDto> GetOutputsByLocal(int idLocal, TransactionStatus? currenStatus, TransactionType? transactionType = null)
        {
            var salidasEntity = SalidaCollectionBussinesAction.Search(CurrentUser.IdEmpresa, idLocal, transactionType.GetCode(), estado: currenStatus.GetStatus());
            var outputs = salidasEntity.Select(x => x.ToOutputLiteDto()).ToList();
            return outputs;
        }


        [HttpGet, Route("warehouses/{idLocal}/products;search={term}"), Route("warehouses/{idLocal}/products/search")]
        public List<LiteProductDto> GetProducts(int idLocal, string term = "all", int? page = null, ProductType? productType = null)
        {
            if (term.Equals("all"))
                term = string.Empty;

            if (term.Length <= 2)
            { throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "La búsqueda  debe ser mayor a 4 caracteres", "búsqueda inválida")); }

            var products = ProductoCollectionBussinesAction.SearchProductsByLocal(CurrentUser.IdEmpresa, idLocal, term, null, null, page, productType.ToString());
            var productsDtoList = products.Select(p => p.ToLiteProductDto()).ToList();

            return productsDtoList;
        }


        [HttpGet, Route("warehouses/transfers/{id}")]
        public List<InputDto> GetTransferById(string id)
        {
            var findParameter = new EntradaFindParameterEntity { IdEmpresa = CurrentUser.IdEmpresa, IdEstado = 1 };


            var salidasEntity = EntradaCollectionBussinesAction.FindByAll(findParameter, 1);
            var outputs = salidasEntity.Select(x => x.ToInputDto()).ToList();
            return outputs;
        }

        [HttpGet, Route("warehouses/{idLocal}/inputs/{currenStatus?}")]
        public List<InputDto> GetInputsByLocal(int idLocal, TransactionStatus? currenStatus = null, TransactionType? transactionType = null)
        {
            var findParameter = new EntradaFindParameterEntity { IdEmpresa = CurrentUser.IdEmpresa, IdEstado = 1 };

            if (idLocal > 0)
            {
                findParameter.IdLocal = idLocal;
            }

            findParameter.EstadoActual = currenStatus.GetStatus();

            if (transactionType != null)
            {
                findParameter.TipoTransaccion = transactionType.GetCode();
            }

            var entradasEntity = EntradaCollectionBussinesAction.FindByAll(findParameter, 1);
            var outputs = entradasEntity.Select(x => x.ToInputDto()).ToList();
            return outputs;
        }

        const string CODIGO_SALIDA = "GE";
        const string CODIGO_ENTRADA = "GR";

        [HttpGet, Route("warehouses/{idLocal}/transfers/search")]
        public List<TransferDto> SearchTransfers(int idLocal, string term = null, string idVendedor = null, DateTime? startDate = null, DateTime? endDate = null, TransactionStatus? status = null)
        {
            var result = new List<TransferDto>();

            var salidasEntity = SalidaCollectionBussinesAction.Search(CurrentUser.IdEmpresa, idLocal, CODIGO_SALIDA, term, idVendedor, default, startDate, endDate, status.GetStatus());
            var entradasEntity = EntradaCollectionBussinesAction.Search(CurrentUser.IdEmpresa, idLocal, CODIGO_ENTRADA, term, idVendedor, default, startDate, endDate, status.GetStatus());

            if (salidasEntity.Count > 0)
                result.AddRange(salidasEntity.Select(x => x.ToTransferDto()).ToList());

            if (entradasEntity.Count > 0)
                result.AddRange(entradasEntity.Select(x => x.ToTransferDto()).ToList());

            return result;
        }



        /// <summary>
        /// Elimina una salida de inventario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost, Route("warehouses/inventory/{id}/receive/{observacion?}")]
        public IHttpActionResult DeleteOutput(string id, string observacion = null)
        {
            try
            {
                var entradaEntity = EntradaBussinesAction.LoadByPK(id);

                if (entradaEntity == null)
                {
                    throw new Exception("PK_Entregada");
                }

                entradaEntity.SetUpdatedState();
                entradaEntity.EstadoActual = "ENTREGADA";
                entradaEntity.FechaModificacion = DateTime.Now;
                entradaEntity.UsuarioModificacion = CurrentUser.InicioSesion;
                entradaEntity.IdEstado = 1;
                entradaEntity.Observaciones = observacion;

                var newSalidaEntity = EntradaBussinesAction.Guardar(entradaEntity);

                if (newSalidaEntity.CurrentState == EntityStatesEnum.SavedSuccessfully)
                {
                    return Ok(true);
                }

                throw new Exception("Hubo un error al guardar el documento");
            }
            catch (HttpResponseException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            {
                string UserError = ex.GetMessage();

                if (ex.GetMessage().Contains("PK_Entrada"))
                {
                    UserError = "No existe la orden especificada";
                }
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
                    $"{ex.GetAllMessages()}", UserError));
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
                    $"{ex.GetAllMessages()}", ex.GetMessage()));
            }
        }

        [HttpPost, Route("warehouses/{id}/review")]
        public OutputDto ReviewOutput(string id, SalesOrderReviewDto model)
        {
            try
            {
                var salida = salesOrderController.GetSalidaEntity(id, model);

                if (salida != null)
                {

                    if (salida.TransaccionPadre != null)
                    {
                        var entrada = EntradaCollectionBussinesAction
                        .FindByAll(new EntradaFindParameterEntity
                        { IdEstado = 1, TransaccionPadre = salida.TransaccionPadre })?.FirstOrDefault();

                        if (entrada != null)
                        {

                            entrada.IpModificacion = salida.IpModificacion ?? salida.IpIngreso;
                            entrada.FechaModificacion = salida.FechaModificacion ?? salida.FechaIngreso;
                            entrada.UsuarioModificacion = salida.UsuarioModificacion ?? salida.UsuarioIngreso;
                            entrada.SetUpdatedState();

                            var detalleEntradaFromIdEntrada = GetDetallesEntity(entrada.Id) ?? new DetalleEntradaEntityCollection();
                            List<InputDetailDto> mdetalle = new List<InputDetailDto>();

                            foreach (var item in detalleEntradaFromIdEntrada)
                            {
                                var mInput = model.Detalles.FirstOrDefault(x => x.IdProducto == item.IdProducto);
                                if (mInput != null)
                                {
                                    InputDetailDto it = new InputDetailDto();

                                    it.IdEmpresa = item.IdEmpresa;
                                    it.CodigoInterno = mInput.CodigoInterno;
                                    it.Id = item.Id;
                                    it.Periodo = item.Periodo;
                                    it.Fecha = item.Fecha;
                                    it.TipoTransaccion = item.TipoTransaccion;
                                    it.IdEntrada = item.IdEntrada;
                                    it.IdProducto = item.IdProducto;
                                    it.IdProveedor = item.IdProveedor;
                                    it.IdLocal = item.IdLocal;
                                    it.Cantidad = mInput.Cantidad;
                                    it.IdTipoUnidad = item.IdTipoUnidad;
                                    it.ValorFOB = item.ValorFOB;
                                    it.FactorDistribucion = item.FactorDistribucion;
                                    it.CostoUnitarioImportacion = item.CostoUnitarioImportacion;
                                    it.CostoVenta = item.CostoVenta;
                                    it.Descuento = item.Descuento;
                                    it.Subtotal = item.Subtotal;
                                    it.ValorAdicional = item.ValorAdicional;
                                    it.FechaFabricacion = item.FechaFabricacion;
                                    it.FechaExpiracion = item.FechaExpiracion;
                                    it.IdEstado = item.IdEstado;
                                    it.FechaIngreso = item.FechaIngreso;
                                    it.UsuarioIngreso = item.UsuarioIngreso;
                                    it.IpIngreso = item.IpIngreso;

                                    mdetalle.Add(it);
                                }
                            }

                            //SetDetallesEntrada(entrada, model.Detalles);
                            SetDetallesEntrada(entrada, mdetalle);
                            EntradaBussinesAction.Save(entrada)?.ToInputDto();

                        }
                    }


                    salida.UpdateEntity(model);
                    salida.EstadoActual = "REVISADA";
                    salida.SetUpdatedState();

                    // Ahora Actualizamos los Detalles:
                    if (model.DetalleEmpaquetado != null)
                    {
                        salesOrderController.SetEmpaquetado(salida, model.DetalleEmpaquetado);
                    }

                    // Ahora Actualizamos los Detalles:
                    if (model.Detalles != null)
                    {
                        salesOrderController.SetDetalles(salida, model.Detalles);

                    }

                    return salesOrderController.GuardarSalida(salida)
                            .ToOutputDto();
                }

                throw new Exception("El numero de pedido especificado no ha sido encontrado.");
            }
            catch (Exception ex)
            {
                if (ex is HttpResponseException)
                {
                    throw ex;
                }
                else
                {
                    throw Request.BuildExceptionResponse(ex);
                }
            }
        }

        public static void SetDetallesEntrada(EntradaEntity entrada, List<InputDetailDto> detalles)
        {
            if (detalles.Count == 0)
            {
                throw new Exception("No se puede eliminar todos los productos, en su lugar debe anular la Orden");
            }
            if (entrada.DetalleEntradaFromIdEntrada == null)
            {
                entrada.DetalleEntradaFromIdEntrada = GetDetallesEntity(entrada.Id) ?? new DetalleEntradaEntityCollection();
            }

            if (detalles.Count > 0)
            {
                var deleted = entrada.DetalleEntradaFromIdEntrada.Where(detalle => !detalles.Any(detail => detail.IdProducto == detalle.IdProducto));

                // Este es un proceso para validar que se esta eliminando los detalles correctos. 
                // De lo contrario no los elimina hasta corregir el tema de los pedidos grandes.

                var del = deleted.Count();
                var cant = detalles.Count;
                var last = entrada.DetalleEntradaFromIdEntrada.Count;
                var rest = last - cant;

                // Los detalles eliminados deben coincidir con 
                // la cantidad de detalles faltantes
                if (rest == del)
                {
                    foreach (var item in deleted)
                    {
                        item.IdEstado = 0;
                        item.FechaModificacion = entrada.FechaModificacion;
                        item.UsuarioModificacion = entrada.UsuarioModificacion;
                        item.IpModificacion = entrada.IpModificacion;
                        item.SetDeletedState();
                    }
                }

                foreach (var detalle in detalles)
                {
                    //var detalleEntity = entrada.DetalleEntradaFromIdEntrada.FirstOrDefault(m => m.Id == entrada.Id);
                    var detalleEntity = entrada.DetalleEntradaFromIdEntrada.FirstOrDefault(m => m.IdProducto == detalle.IdProducto);

                    if (detalleEntity != null)
                    {
                        // Actualizo los detalles con los datos enviados
                        detalleEntity.UpdateEntity(detalle);
                        detalleEntity.IdEntrada = entrada.Id;
                        detalleEntity.Periodo = entrada.Periodo;
                        detalleEntity.TipoTransaccion = entrada.TipoTransaccion;
                        detalleEntity.IdEstado = 1;
                        detalleEntity.Fecha = entrada.Fecha;
                        detalleEntity.FechaModificacion = entrada.FechaModificacion;
                        detalleEntity.UsuarioModificacion = entrada.UsuarioModificacion;
                        detalleEntity.IpModificacion = entrada.IpModificacion;
                        detalleEntity.SetUpdatedState();
                    }
                    else
                    {
                        detalleEntity = detalle.ToDetalleEntradaEntity();
                        detalleEntity.IdEntrada = entrada.Id;
                        detalleEntity.Periodo = entrada.Periodo;
                        detalleEntity.TipoTransaccion = entrada.TipoTransaccion;
                        detalleEntity.IdEstado = 1;
                        detalleEntity.Fecha = entrada.Fecha;
                        detalleEntity.FechaIngreso = entrada.FechaModificacion ?? entrada.FechaIngreso;
                        detalleEntity.UsuarioIngreso = entrada.UsuarioModificacion ?? entrada.UsuarioIngreso;
                        detalleEntity.IpIngreso = entrada.IpModificacion ?? entrada.IpIngreso;
                        detalleEntity.SetNewState();

                        entrada.DetalleEntradaFromIdEntrada.Add(detalleEntity);
                    }
                }
            }
        }

        static DetalleEntradaEntityCollection GetDetallesEntity(string idEntrada)
        {
            return DetalleEntradaCollectionBussinesAction.FindByAll(new DetalleEntradaFindParameterEntity { IdEntrada = idEntrada, IdEstado = 1 }, 0); ;
        }

    }
}