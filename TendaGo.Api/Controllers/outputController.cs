using ER.BA;
using ER.BE;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;
using TendaGo.Api.Reporting;
using TendaGo.Common;

namespace TendaGo.Api.Controllers
{
    [TokenAuthorize]
    public class outputController : ApiControllerBase
    {
        public List<OutputDto> GetOutputs()
        {
            var outputs = SalidaCollectionBussinesAction.FindByAll(new SalidaFindParameterEntity { IdEmpresa = CurrentUser.IdEmpresa }, 0);

            // Cargo la lista de clientes
            var customers = EntidadCollectionBussinesAction.FindByAll(new EntidadFindParameterEntity(), 0);
            outputs.ForEach(x => x.IdClienteAsEntidad = customers.FirstOrDefault(m => m.Id == x.IdCliente));

            var outputsDtoList = outputs
                .Select(br => br.ToOutputLiteDto())
                .ToList();

            return outputsDtoList;
        }

        [HttpPost, Route("output/report")]
        public List<OutputsReportDto> GetReport(SearchOutputReport parametros)
        {

            SalidaFindParameterEntity ef = CatalogsExtensions.GlobalMapperConverter<SearchOutputReport, SalidaFindParameterEntity>(parametros);
            DataSet outputs = SalidaCollectionBussinesAction.ReporteSalidas(ef);
            return outputs.ConvertirToDto<OutputsReportDto>().ToList();
        }

        [HttpGet, Route("output;status={currenStatus}")]
        public List<OutputDto> GetOutputsByCurrentStatus(TransactionStatus? currenStatus, TransactionType? transactionType = null)
        {
            //int statusConverted;
            bool isValidId = (currenStatus != null);//int.TryParse(currenStatus, out statusConverted);

            if (!isValidId)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro id, es invalido", "Id invalido"));

            var findParameter = new SalidaFindParameterEntity { IdEmpresa = CurrentUser.IdEmpresa };
            findParameter.EstadoActual = currenStatus.GetStatus();

            if (transactionType != null)
            {
                findParameter.TipoTransaccion = transactionType.GetCode();
            }

            var salidasEntity = ER.BA.SalidaCollectionBussinesAction.FindByAll(findParameter, 1);
            var outputs = salidasEntity.Select(x => x.ToOutputLiteDto()).ToList();
            return outputs;
        }

        public OutputDto GetOutput(string id)
        {
            try
            {
                var output = GetSalidaEntity(id)?
                                .ToOutputDto();

                //if (output.CuentasPorCobrar.Count > 0 && output.EstadoActual != "REVISADA")
                if (output.CuentasPorCobrar.Count > 0)
                {
                    foreach (var cxc in output.CuentasPorCobrar)
                    {
                        cxc.MetodosPago = new List<ReceivablePayMethodDto>();
                        var metodos = DetalleCobroCollectionBussinesAction.FindByAll(new DetalleCobroFindParameterEntity { IdCuentaPorCobrar = cxc.Id, IdEstado = 1 });
                        foreach (var meth in metodos)
                        {
                            cxc.MetodosPago.Add(new ReceivablePayMethodDto()
                            {
                                IdMedioPago = meth.IdMedioPago,
                                Valor = meth.Valor,
                                MetodoPago = meth.IdMedioPago_DisplayMember,
                                Descripcion = meth.Descripcion,
                                Id = meth.Id,
                                IdCobroDebito = meth.IdCobroDebito,
                                IdCierreCaja = meth.IdCierreCaja,
                                Estado = true,
                                ValorOriginal = meth.Valor
                            });
                        }
                    }
                }

                //Cargo el valor de notas de Credito
                if (output.EstadoActual == "EN PROCESO")
                    output.ValorNc = CuentaPorCobrarBussinesAction.ValorNotaCreditoByCliente(output.IdCliente);

                if (!string.IsNullOrEmpty(output.TransaccionPadre))
                {
                    output.Entrada = EntradaCollectionBussinesAction
                        .FindByAll(new EntradaFindParameterEntity { TransaccionPadre = output.TransaccionPadre })?
                        .FirstOrDefault()?.ToInputDto();
                }

                return output;
            }
            catch (HttpResponseException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            {
                string userError = ex.GetMessage();

                if (userError.Contains("PK_Salida"))
                {
                    userError = "No existe la salida especificada";
                }
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
                    ex.GetAllMessages(), userError));
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
                    ex.GetAllMessages(), ex.GetMessage()));
            }
        }


        /// <summary>
        /// Crea una salida de inventario
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("output/create")]
        public OutputDto PostOutput(OutputCreateRequestModel model)
        {
            var salidaJson = JsonConvert.SerializeObject(model);

            var salida = model.ToSalidaEntity();

            SetCobroDebito(salida, model.CuentasPorCobrar);
            SetInfoAdic(salida, model.InformacionAdicional);

            var result = GuardarSalida(salida);

            return result?.ToOutputDto();
        }

        /// <summary>
        /// Devuelve salidas de pagetes por salida
        /// </summary>
        /// <param name="idSalida"></param>
        /// <returns></returns>
        [HttpGet, Route("output/{idSalida}/package")]
        public List<EmpaquetadoEntity> GetPackage(string idSalida)
        {
            var packages = EmpaquetadoCollectionBussinesAction.FindByAll(new EmpaquetadoFindParameterEntity { IdSalida = idSalida, IdEstado = 1 }, 0); ;
            List<EmpaquetadoEntity> pac = new List<EmpaquetadoEntity>();

            if (packages != null && packages.Count() > 0)
            {
                foreach (var val in packages)
                {
                    pac.Add(val);
                }
            }

            return pac;
        }

        private void SetInfoAdic(SalidaEntity salida, List<AditionalInfoDto> infAdicional)
        {
            if (infAdicional != null)
            {
                foreach (var inf in infAdicional)
                {
                    if (salida.InfoAdicionalFromIdSalida == null)
                    {
                        salida.InfoAdicionalFromIdSalida = new InfoAdicionalEntityCollection();
                    }

                    var detalle = new InfoAdicionalEntity
                    {
                        TituloInfoAdicional = inf.TituloInfoAdicional.ToUpper(),
                        InfoAdicional = inf.InfoAdicional.ToUpper()
                    };

                    detalle.SetNewState();

                    salida.InfoAdicionalFromIdSalida.Add(detalle);
                }
            }

        }


        private CobroDebitoEntity CrearRecibo(SalidaEntity salida, string motivo)
        {
            var recibo = new CobroDebitoEntity
            {
                IdCliente = salida.IdCliente,
                Fecha = salida.FechaModificacion ?? salida.FechaIngreso,
                Detalle = motivo,
                IdEmpresa = salida.IdEmpresa,
                IdEstado = 1,
                UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso,
                IpIngreso = salida.IpModificacion ?? salida.IpIngreso,
                FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso
            };

            recibo.SetNewState();
            recibo.DetalleCobroFromIdCobroDebito = new DetalleCobroEntityCollection();
            recibo.DetalleMedioCobroFromIdCobroDebito = new DetalleMedioCobroEntityCollection();
            return recibo;
        }

        private void SetCobroDebito(SalidaEntity salida, List<ReceivableDto> cuentasPorCobrar)
        {
            //Si es cotizacion no generamos cobros
            if (salida.TipoTransaccion == "CT")
                return;

            salida.FechaIngreso = DateTime.Now;

            if ((cuentasPorCobrar?.Count ?? 0) == 0)
            {
                throw new Exception("Debe especificar la información de los pagos de la orden.");
            }

            foreach (var cxc in cuentasPorCobrar.Where(x => x.ValorPagado > 0))
            {
                var cxcEntity = salida.CuentaPorCobrarFromIdSalida.FirstOrDefault(m => m.Numero == cxc.Numero);
                if (cxcEntity != null)
                {
                    if (salida.ReciboCobro == null)
                    {
                        salida.ReciboCobro = CrearRecibo(salida, $"ORDEN # {salida.Id}. {salida.Observaciones}");
                    }

                    cxcEntity.DetalleCobroFromIdCuentaPorCobrar = cxcEntity.DetalleCobroFromIdCuentaPorCobrar ?? new DetalleCobroEntityCollection();

                    //var detalle1 = new DetalleCobroEntity
                    //{
                    //    IdCobroDebito = salida.ReciboCobro.Id,
                    //    IdCobroDebitoAsCobroDebito = salida.ReciboCobro,
                    //    IdCuentaPorCobrar = cxcEntity.Id,
                    //    IdCuentaPorCobrarAsCuentaPorCobrar = cxcEntity,
                    //    IdEstado = 1,
                    //    IdLocal = salida.IdLocal,

                    //    Valor = cxc.Valor,

                    //    FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso,
                    //    UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso,
                    //    IpIngreso = salida.IpModificacion ?? salida.IpIngreso
                    //};

                    //detalle1.SetNewState();

                    //cxcEntity.DetalleCobroFromIdCuentaPorCobrar.Add(detalle1);
                    //salida.ReciboCobro.DetalleCobroFromIdCobroDebito.Add(detalle1);

                    if (cxc.MetodosPago != null) foreach (var metodo in cxc.MetodosPago)
                        {

                            // Hay que crear un detalle para sustentar el pago.
                            var detalle = new DetalleCobroEntity
                            {
                                IdCobroDebito = salida.ReciboCobro.Id,
                                IdCobroDebitoAsCobroDebito = salida.ReciboCobro,
                                IdCuentaPorCobrar = cxcEntity.Id,
                                IdCuentaPorCobrarAsCuentaPorCobrar = cxcEntity,
                                IdEstado = 1,
                                IdLocal = salida.IdLocal,

                                IdMedioPago = metodo.IdMedioPago,
                                Valor = metodo.Valor,
                                Descripcion = metodo.Descripcion,

                                FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso,
                                UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso,
                                IpIngreso = salida.IpModificacion ?? salida.IpIngreso
                            };

                            detalle.SetNewState();

                            cxcEntity.DetalleCobroFromIdCuentaPorCobrar.Add(detalle);
                            salida.ReciboCobro.DetalleCobroFromIdCobroDebito.Add(detalle);


                            // Si se ha creado el detalle sin guardar el medio                            
                            var medioCobro = new DetalleMedioCobroEntity
                            {
                                IdCobroDebito = salida.ReciboCobro.Id,
                                IdEstado = 1,
                                IdMedioPago = metodo.IdMedioPago,
                                Valor = metodo.Valor,
                                Descripcion = metodo.Descripcion,

                                UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso,
                                FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso,
                                IpIngreso = salida.IpModificacion ?? salida.IpIngreso,

                            };

                            medioCobro.SetNewState();
                            salida.ReciboCobro.DetalleMedioCobroFromIdCobroDebito.Add(medioCobro);


                        }
                    else
                    {
                        // Hay que crear un detalle para sustentar el pago.
                        var detalle = new DetalleCobroEntity
                        {
                            IdCobroDebito = salida.ReciboCobro.Id,
                            IdCobroDebitoAsCobroDebito = salida.ReciboCobro,
                            IdCuentaPorCobrar = cxcEntity.Id,
                            IdCuentaPorCobrarAsCuentaPorCobrar = cxcEntity,
                            IdEstado = 1,
                            IdLocal = salida.IdLocal,
                            IdMedioPago = cxc.IdMedioPago == 0 ? 1 : cxc.IdMedioPago,
                            Valor = cxc.ValorPagado,
                            FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso,
                            UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso,
                            IpIngreso = salida.IpModificacion ?? salida.IpIngreso
                        };

                        detalle.SetNewState();

                        cxcEntity.DetalleCobroFromIdCuentaPorCobrar.Add(detalle);
                        salida.ReciboCobro.DetalleCobroFromIdCobroDebito.Add(detalle);
                    }


                }
            }
        }


        /// <summary>
        /// Crea una salida de inventario
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("output/detail/{id}")]
        public void CheckDetail(string id, bool? empaquetado, bool? revisado)
        {
            DetalleSalidaBussinesAction.Check(id, empaquetado, revisado);
        }

        /// <summary>
        /// Carga varias salidas de inventario
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("output/load")]
        public List<OutputDto> PostOutputLoad(OutputLoadModel model)
        {
            try
            {
                if (model == null || model.ListaPedidos == null)
                {
                    throw new Exception("No se ha especificado las ordenes para guardar!");
                }

                using (var connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                    {
                        try
                        {
                            var ordenes = new List<OutputDto>();

                            foreach (var output in model.ListaPedidos)
                            {
                                output.TipoTransaccionPadre = model.TipoTransaccionPadre;
                                output.TransaccionPadre = model.TransaccionPadre;
                                output.FechaIngreso = model.FechaIngreso;
                                output.UsuarioIngreso = model.UsuarioIngreso;
                                output.IpIngreso = model.IpIngreso;

                                var salida = output.ToSalidaEntity();

                                SetCobroDebito(salida, output.CuentasPorCobrar);

                                var result = GuardarSalida(salida, connection, transaction);

                                if (result != null)
                                {
                                    ordenes.Add(result.ToOutputLiteDto());
                                }
                                else
                                {
                                    throw new Exception("Hubo un error al procesar los pedidos");
                                }
                            }

                            transaction.Commit();

                            return ordenes;
                        }
                        catch (Exception exc)
                        {
                            if (transaction != null)
                            {
                                transaction.Rollback();
                            }

                            throw exc;
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            }
            catch (HttpResponseException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            {
                string userError = ex.GetMessage();

                if (userError.Contains("PK_Salida"))
                {
                    userError = "No existe la salida especificada";
                }
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
                    ex.GetAllMessages(), userError));
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
                    ex.GetAllMessages(), ex.GetMessage()));
            }

        }




        /// <summary>
        /// Elimina una salida de inventario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost, Route("output/{id}/delete")]
        public IHttpActionResult DeleteOutput(string id, OutputDeleteDto dto)
        {
            try
            {
                var salidaEntity = GetSalidaEntity(id);

                if (salidaEntity == null)
                {
                    throw new Exception("PK_Salida");
                }

                salidaEntity.SetDeletedState();
                salidaEntity.EstadoActual = "ANULADA";
                salidaEntity.FechaModificacion = dto.FechaIngreso;
                salidaEntity.UsuarioModificacion = dto.UsuarioIngreso;
                salidaEntity.IpModificacion = dto.IpIngreso;
                salidaEntity.Observaciones = $"ORDEN ANULADA: {dto.Observaciones} {dto.UsuarioIngreso} {dto.FechaIngreso} - {salidaEntity.Observaciones}";
                salidaEntity.IdEstado = 0;

                var newSalidaEntity = SalidaBussinesAction.Guardar(salidaEntity);

                if (newSalidaEntity.CurrentState == EntityStatesEnum.SavedSuccessfully)
                {
                    return Ok(true);
                }

                throw new Exception("Hubo un error al eliminar el documento");
            }
            catch (HttpResponseException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            {
                string UserError = ex.GetMessage();

                if (UserError.Contains("PK_Salida"))
                {
                    UserError = "No existe la salida especificada";
                }
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
                   ex.GetAllMessages(), UserError));
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
                    ex.GetAllMessages(), ex.GetMessage()));
            }
        }


        private SalidaEntity GuardarSalida(SalidaEntity salidaEntity, SqlConnection connection = null, SqlTransaction transaction = null)
        {

            try
            {
                if (salidaEntity.DetalleSalidaFromIdSalida == null || salidaEntity.DetalleSalidaFromIdSalida.Count == 0)
                {
                    throw new Exception("No se puede guardar un pedido sin detalles, por favor revise su orden.");
                }

                salidaEntity.IdEmpresa = CurrentUser.IdEmpresa;

                if (string.IsNullOrEmpty(salidaEntity.Id))
                {
                    salidaEntity.SetNewState();
                    salidaEntity.TipoTransaccion = string.IsNullOrEmpty(salidaEntity.TipoTransaccion) ? "NP" : salidaEntity.TipoTransaccion;
                    salidaEntity.Fecha = DateTime.Now;
                    salidaEntity.Periodo = salidaEntity.Fecha.ToString("yyyyMM");
                    salidaEntity.EstadoActual = string.IsNullOrEmpty(salidaEntity.EstadoActual) ? "EN PROCESO" : salidaEntity.EstadoActual;
                    salidaEntity.FechaIngreso = DateTime.Now;
                    salidaEntity.IdEstado = 1;
                    salidaEntity.Total = salidaEntity.DetalleSalidaFromIdSalida.Sum(x => x.Subtotal) + salidaEntity.ValorAdicional;
                    salidaEntity.Saldo = salidaEntity.Total;

                    if (salidaEntity.Descuento > 0)
                    {
                        var descuento = Math.Round((salidaEntity.Total * salidaEntity.Descuento) / 100, 2);
                        salidaEntity.Total = salidaEntity.Total - descuento;
                        salidaEntity.Saldo = salidaEntity.Saldo - descuento;
                    }

                }
                else
                {
                    salidaEntity.SetUpdatedState();
                }

                //Configuro el establecimiento y el punto de emision de la factura electronica 
                if (salidaEntity.DocumentoFromIdSalida != null && salidaEntity.DocumentoFromIdSalida.Count != 0)
                {
                    foreach (var item in salidaEntity.DocumentoFromIdSalida)
                    {
                        item.IdEmpresa = salidaEntity.IdEmpresa;
                        item.Fecha = item.Fecha.AddHours(-5);

                        var LocalBodega = LocalBodegaBussinesAction.LoadByPK(salidaEntity.IdLocal);
                        if (LocalBodega.Establecimiento != null && LocalBodega.PuntoEmision != null)
                        {
                            item.Establecimiento = LocalBodega.Establecimiento;
                            item.PuntoEmision = LocalBodega.PuntoEmision;

                            //Calculo el descuento
                            if (salidaEntity.Descuento > 0)
                            {
                                //var subtotal = salidaEntity.DetalleSalidaFromIdSalida.Sum(x => x.Subtotal) + salidaEntity.ValorAdicional;
                                //item.TotalDescuento = Math.Round((subtotal * salidaEntity.Descuento) / 100,2);                                
                                item.TotalDescuento = salidaEntity.Descuento;
                            }
                        }
                    }
                }

                // Configuro el metodo que genera las facturas electrónicas
                salidaEntity.ProcesarFacturaHandler = OutputExtensions.GenerarFacturas;

                var newSalidaEntity = SalidaBussinesAction.Guardar(salidaEntity, connection, transaction);
                return newSalidaEntity;
            }
            catch (HttpResponseException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            {
                string userError = ex.GetMessage();

                if (userError.Contains("PK_Salida"))
                {
                    userError = "No existe la salida especificada";
                }
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
                   ex.GetAllMessages(), userError));
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
                    ex.GetAllMessages(), ex.GetMessage()));
            }
        }


        [HttpPost, Route("output/{idSalida}/{IdLocalSelected}/changeLocal")]
        public OutputDto PostOutputChange(string idSalida, int IdLocalSelected, OutputCreateRequestModel model)
        {
            var connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);
            connection.Open();
            var transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

            try
            {

                //Transferencia
                TransferRequest transfer = new TransferRequest();
                transfer.Fecha = DateTime.Now;
                transfer.EstadoActual = "EN PROCESO";
                transfer.IdLocalDestino = model.IdLocal;
                transfer.IdVendedor = User.Identity.Name;
                transfer.IdEmpresa = CurrentUser.IdEmpresa;

                //Valido el stock del producto            
                var productos = model.DetalleNotaPedido.Select(x => x.IdProducto).JoinString(",");

                var stocks = StockCollectionBussinesAction.SaldoInventario(CurrentUser.IdEmpresa, productos, model.IdLocal);

                //Si no existe stock de produto lo añado en stock 0
                foreach (var rec in model.DetalleNotaPedido)
                {
                    if (stocks == null)
                        stocks = new ProductoEntityCollection();

                    if (!stocks.Any(cus => cus.Id == rec.IdProducto))
                    {
                        var noStock = new ProductoEntity { Id = rec.IdProducto, Stock = 0 };
                        stocks.Add(noStock);
                    }
                }

                //if (stocks == null)
                //    stocks = new ProductoEntityCollection();

                transfer.Detalle = model.DetalleNotaPedido.Where(detalle => stocks == null || stocks.Any(x => x.Id == detalle.IdProducto && detalle.Cantidad > x.Stock))
                                                .Select(prod => new TransferDetailDto
                                                {
                                                    Cantidad = prod.Cantidad - (stocks?.FirstOrDefault(t => t.Id == prod.IdProducto)?.Stock ?? 0),
                                                    IdLocal = IdLocalSelected,
                                                    IdProducto = prod.IdProducto,
                                                    IdTipoUnidad = prod.IdTipoUnidad
                                                }).ToList();


                var salida = model.ToSalidaEntity();
                salida.Id = idSalida;
                salida.FechaModificacion = DateTime.Now;
                salida.UsuarioModificacion = User.Identity.Name;
                salida.IpModificacion = "1.1.1.1";
                salida.TipoTransaccion = "NP";
                salida.IdLocal = IdLocalSelected;
                salida.IdEmpresa = CurrentUser.IdEmpresa;
                salida.CuentaPorCobrarFromIdSalida = null;
                salida.ReciboCobro = null;
                salida.BorrarCobros = true;
                salida.SetDeletedState();
                salida.DetalleSalidaFromIdSalida
                    .ToList()
                    .ForEach(c => c.CurrentState = salida.CurrentState);

                salida = SalidaBussinesAction.Guardar(salida, connection, transaction);

                salida.IdLocal = model.IdLocal;
                //if (transaction != null)
                //    transaction.Commit();


                ////Nuevo commit 
                //connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);
                //connection.Open();
                //transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

                //Si no hay stock hago un pedido a bodega
                if (transfer.Detalle.Any())
                {
                    transfer.FechaIngreso = DateTime.Now;
                    transfer.UsuarioIngreso = User.Identity.Name;
                    transfer.IpIngreso = "1.1.1";

                    warehousesController.Transferir(transfer, connection, transaction);
                }

                var result = SalidaBussinesAction.Cambio(salida, connection, transaction);

                if (transaction != null)
                    transaction.Commit();

                return result?.ToOutputDto();

            }
            catch (HttpResponseException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            {
                string userError = ex.GetMessage();

                if (userError.Contains("PK_Salida"))
                {
                    userError = "No existe la salida especificada";
                }
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
                   ex.GetAllMessages(), userError));
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
                    ex.GetAllMessages(), ex.GetMessage()));
            }
            finally
            {
                connection.Close();
            }

        }


        internal SalidaEntity GetSalidaEntity(string id)
        {
            var salidaEntity = SalidaBussinesAction.LoadByPK(id, 1);

            // Aqui hago dos validaciones, tiene que existir la salida y pertenecer a la empresa actual. 
            // De lo contrario no se admite por cuestion de permisos.
            if (salidaEntity == null || salidaEntity.IdEmpresa != CurrentUser?.IdEmpresa)
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, "Salida no existe", "La salida solicitada no existe"));

            // Si existe la salida entonces cargo los detalles del documento
            salidaEntity.DetalleSalidaFromIdSalida = DetalleSalidaCollectionBussinesAction.FindByAll(new DetalleSalidaFindParameterEntity { IdSalida = salidaEntity.Id, IdEstado = 1 }); ;
            salidaEntity.EmpaquetadoFromIdSalida = EmpaquetadoCollectionBussinesAction.FindByAll(new EmpaquetadoFindParameterEntity { IdSalida = salidaEntity.Id, IdEstado = 1 });
            salidaEntity.CuentaPorCobrarFromIdSalida = CuentaPorCobrarCollectionBussinesAction.FindByAll(new CuentaPorCobrarFindParameterEntity { IdSalida = salidaEntity.Id, IdEstado = 1 });
            salidaEntity.DocumentoFromIdSalida = DocumentoCollectionBussinesAction.FindByAll(new DocumentoFindParameterEntity { IdSalida = salidaEntity.Id, IdEstado = 1 }, 0);


            return salidaEntity;
        }

        [HttpPost, Route("output/{id}/deleteChange")]
        public IHttpActionResult DeleteOutputChange(string id, OutputDeleteDto dto)
        {
            try
            {
                var salidaEntity = GetSalidaEntity(id);

                if (salidaEntity == null)
                {
                    throw new Exception("PK_Salida");
                }

                salidaEntity.SetDeletedState();
                salidaEntity.EstadoActual = "ANULADA";
                salidaEntity.FechaModificacion = dto.FechaIngreso;
                salidaEntity.UsuarioModificacion = dto.UsuarioIngreso;
                salidaEntity.IpModificacion = dto.IpIngreso;
                salidaEntity.Observaciones = $"ORDEN ANULADA: {dto.Observaciones} {dto.UsuarioIngreso} {dto.FechaIngreso} - {salidaEntity.Observaciones}";
                salidaEntity.IdEstado = 0;


                var cuentasCobrar = CuentaPorCobrarCollectionBussinesAction.FindByAll(new CuentaPorCobrarFindParameterEntity
                {
                    IdSalida = salidaEntity.Id,
                    IdEstado = 1
                });

                if (cuentasCobrar != null)
                {
                    salidaEntity.CuentaPorCobrarFromIdSalida = new CuentaPorCobrarEntityCollection();

                    foreach (CuentaPorCobrarEntity cxc in cuentasCobrar)
                    {
                        cxc.CurrentState = EntityStatesEnum.Deleted;
                        cxc.FechaModificacion = dto.FechaIngreso;
                        cxc.UsuarioModificacion = dto.UsuarioIngreso;
                        cxc.IpModificacion = dto.IpIngreso;
                        var detalleCobro = DetalleCobroCollectionBussinesAction.FindByAll(new DetalleCobroFindParameterEntity
                        {
                            IdCuentaPorCobrar = cxc.Id,
                            IdEstado = 1
                        });

                        if (cxc.DetalleCobroFromIdCuentaPorCobrar == null)
                        {
                            cxc.DetalleCobroFromIdCuentaPorCobrar = new DetalleCobroEntityCollection();
                        }

                        foreach (DetalleCobroEntity dp in detalleCobro)
                        {
                            dp.CurrentState = EntityStatesEnum.Deleted;
                            dp.FechaModificacion = dto.FechaIngreso;
                            dp.UsuarioModificacion = dto.UsuarioIngreso;
                            dp.IpModificacion = dto.IpIngreso;
                            cxc.DetalleCobroFromIdCuentaPorCobrar.Add(dp);

                            CobroDebitoEntity rec = CobroDebitoBussinesAction.LoadByPK(dp.IdCobroDebito);

                            if (rec != null)
                            {
                                if (salidaEntity.ReciboCobro == null)
                                {
                                    salidaEntity.ReciboCobro = new CobroDebitoEntity();
                                }

                                salidaEntity.ReciboCobro.Id = dp.IdCobroDebito;
                                salidaEntity.ReciboCobro.CurrentState = EntityStatesEnum.Deleted;
                                salidaEntity.ReciboCobro.FechaModificacion = dto.FechaIngreso;
                                salidaEntity.ReciboCobro.UsuarioModificacion = dto.UsuarioIngreso;
                                salidaEntity.ReciboCobro.IpModificacion = dto.IpIngreso;
                            }

                        }

                        salidaEntity.CuentaPorCobrarFromIdSalida.Add(cxc);
                    }
                }

                var newSalidaEntity = SalidaBussinesAction.Guardar(salidaEntity);

                if (newSalidaEntity.CurrentState == EntityStatesEnum.SavedSuccessfully)
                {
                    return Ok(true);
                }

                throw new Exception("Hubo un error al eliminar el documento");
            }
            catch (HttpResponseException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            {
                string UserError = ex.GetMessage();

                if (UserError.Contains("PK_Salida"))
                {
                    UserError = "No existe la salida especificada";
                }
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
                   ex.GetAllMessages(), UserError));
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
                    ex.GetAllMessages(), ex.GetMessage()));
            }
        }


        [HttpGet, Route("output/estadisticas")]
        public StatisticsDto Estadisticas()
        {

            var estadistic = EstadisticaBussinesAction.LoadByPK(CurrentUser.IdEmpresa, 1);
            var result = estadistic.ToStatisticsDto();

            return result;
        }

        [HttpGet, Route("output/estadisticasMensual")]
        public List<StatisticsMonthDto> EstadisticasMensual()
        {
            var outputs = EstadisticaBussinesAction.LoadMesByPK(CurrentUser.IdEmpresa, 1);
            var outputsDtoList = outputs
                .Select(br => br.ToStatisticsMonthDto())
                .ToList();

            return outputsDtoList;
        }

        [HttpGet, Route("output/estadisticasMensualResumida")]
        public StatisticsMonthDto EstadisticasMensualResumida()
        {
            var outputs = EstadisticaBussinesAction.LoadMesByPK(CurrentUser.IdEmpresa, 1);
            var totalMes = outputs.Sum(x => x.Total);

            StatisticsMonthDto outputsDto = new StatisticsMonthDto
            {
                Fecha = DateTime.Now,
                Total = totalMes
            };

            return outputsDto;
        }

        [HttpGet, Route("output/estadisticasCobro")]
        public List<StatisticsMonthDto> EstadisticasCobro()
        {

            var outputs = EstadisticaBussinesAction.LoadCobroByPK(CurrentUser.IdEmpresa, 1);
            List<StatisticsMonthDto> outp = new List<StatisticsMonthDto>();

            if (outputs != null)
            {
                var outputsDtoList = outputs
                .Select(br => br.ToStatisticsMonthDto())
                .ToList();
                return outputsDtoList;
            }

            return outp;
        }

        [HttpPost, Route("output/{id}/{email}/enviarCorreo")]
        public async Task<IHttpActionResult> Correo(string id, string email)
        {
            // Obtengo el generador de reportes
            var builder = new ReportBuilder("/tendago/Salida");

            // Configuro los parametros del reporte
            builder.AddParameter("IdSalida", id);

            // Genero el reporte
            var report = builder.Render(Convert.ToString(ReportFormatEnum.PDF));

            var memStream = new MemoryStream(report.Content);
            memStream.Position = 0;
            var contentType = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
            var reportAttachment = new Attachment(memStream, contentType);
            reportAttachment.ContentDisposition.FileName = id.Trim() + ".pdf";

            //mailMessage.Attachments.Add(reportAttachment);
            //attach.ContentDisposition.FileName = id.Trim()+".pdf";


            await Tools.SendMailAsync(email, "Nota de Pedido", "<h3> " + id + " </h3>", reportAttachment);
            return Ok();
        }
    }
}
