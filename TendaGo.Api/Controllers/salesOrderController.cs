using ER.BA;
using ER.BE;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TendaGo.Common;

namespace TendaGo.Api.Controllers
{
    [TokenAuthorize]
    public class salesOrderController : ApiControllerBase
    {
        [HttpPost, Route("salesOrder/{id}/approve")]
        public async Task<OutputDto> ApproveOutput(string id, SalesOrderApprovalDto model)
        {
            try
            {
                var modelJson = model.ToJson();

                var salida = GetSalidaEntity(id, model);

                if (salida != null)
                {
                    salida.UpdateEntity(model);

                    if (model.Entrega == "INMEDIATA" || salida.EstadoActual == "COTIZACION")
                    {
                        salida.EstadoActual = "ENTREGADA";
                        salida.TipoTransaccion = "NP";
                    }
                    else
                    {
                        salida.EstadoActual = "APROBADA";
                    }

                    salida.SetUpdatedState();

                    // Ahora Actualizamos los Detalles:
                    if (model.Detalles != null)
                    {
                        SetDetalles(salida, model.Detalles);
                    }

                    if (model.CuentasPorCobrar != null)
                    {
                        //solo entregar si es contado y esté todo pagado
                        if ((salida.IdFormaPago != 1 || model.CuentasPorCobrar.Sum(x => x.Saldo) > 0) && salida.EstadoActual == "ENTREGADA")
                        {
                            throw new Exception("Entrega inmeditada no es contado o no ha sido pagado totalmente");
                        }

                        await SetCuentasPorCobrarAprobar(salida, model.CuentasPorCobrar, null);

                    }

                    //Cambio el usuario que realizo el pago desde la app por el usuario que aprobó en la web
                    if (salida.IdEmpresa != 1)
                    {
                        foreach (var item in salida.CuentaPorCobrarFromIdSalida)
                        {
                            foreach (var detalle in item.DetalleCobroFromIdCuentaPorCobrar)
                            {
                                detalle.UsuarioIngreso = salida.UsuarioModificacion;
                            }
                        }
                    }
                    var salidaJson = salida.ToJson();

                    return GuardarSalida(salida)
                                .ToOutputDto();
                }

                throw new Exception("El numero de pedido especificado no ha sido encontrado.");
            }
            catch (HttpResponseException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw Request.BuildExceptionResponse(ex);
            }
        }

        [HttpPost, Route("salesOrder/{id}/packing")]
        public OutputDto PackingOutput(string id, SalesOrderPackingDto model)
        {
            try
            {
                var salida = GetSalidaEntity(id, model);

                if (salida != null)
                {
                    salida.UpdateEntity(model);
                    salida.EstadoActual = "EMPAQUETADA";
                    salida.SetUpdatedState();

                    // Ahora Actualizamos los Detalles:
                    if (model.DetalleEmpaquetado != null)
                    {
                        SetEmpaquetado(salida, model.DetalleEmpaquetado);
                    }

                    return GuardarSalida(salida).ToOutputDto();
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


        [HttpPost, Route("salesOrder/{id}/review")]
        public async Task<OutputDto> ReviewOutput(string id, SalesOrderReviewDto model)
        {
            try
            {
                var salida = GetSalidaEntity(id, model);

                if (salida != null)
                {
                    salida.UpdateEntity(model);
                    salida.EstadoActual = "REVISADA";
                    salida.SetUpdatedState();

                    // Ahora Actualizamos los Detalles:
                    if (model.DetalleEmpaquetado != null)
                    {
                        SetEmpaquetado(salida, model.DetalleEmpaquetado);
                    }

                    // Ahora Actualizamos los Detalles:
                    if (model.Detalles != null)
                    {
                        SetDetalles(salida, model.Detalles);
                    }

                    if (model.CuentasPorCobrar != null)
                    {
                        await SetCuentasPorCobrar(salida, model.CuentasPorCobrar, null);
                    }

                    return GuardarSalida(salida)
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


        [HttpPost, Route("salesOrder/{id}/deliver")]
        public async Task<OutputDto> DeliverOutput(string id, SalesOrderDeliverDto model)
        {
            try
            {
                var salida = GetSalidaEntity(id, model);


                if (salida != null)
                {
                    salida.UpdateEntity(model);
                    salida.FechaHoraEntregaReal = DateTime.Now;
                    salida.EstadoActual = "ENTREGADA";
                    salida.SetUpdatedState();


                    // Ahora Actualizamos los Detalles:
                    if (model.Detalles != null)
                    {
                        SetDetalles(salida, model.Detalles);
                    }

                    if (model.CuentasPorCobrar != null)
                    {
                        //solo entregar si es contado y esté todo pagado
                        var pagos = model.CuentasPorCobrar.Sum(x => x.MetodosPago.Sum(p => p.Valor));
                        var recibo = salida;
                        if (salida.IdFormaPago != 1 || model.CuentasPorCobrar.Sum(x => x.Saldo) > 0)
                        {
                            throw new Exception("El numero de pedido no es contado o no ha sido pagado totalmente");
                        }


                        //Valida que en Contado no guarde un total de pagos menos al valor de la cuota.
                        //if (model.IdFormaPago == 1 && model.CuentasPorCobrar.Count == 1 )
                        //{
                        //    var val = model.CuentasPorCobrar[0].MetodosPago.Sum(x =>  x.Valor);
                        //    if (val < model.CuentasPorCobrar[0].Valor)
                        //    {
                        //        throw new Exception($"La sumatoria de los pagos ${val} no es igual al valor de la cuota ${model.CuentasPorCobrar[0].Valor}");
                        //    }
                        //}

                        var cuentasJson = model.CuentasPorCobrar.ToJson();

                        await SetEntregaCuentasPorCobrar(salida, model.CuentasPorCobrar, null);

                        //CODIGO COMENTADO PORQUE AHORA QUIERE REFLEJAR SALDOS NEGATIVOS EN CIERRE DE CAJA
                        // Debe validar que la cuenta por cobrar haya sido saldada.
                        //if (model.CuentasPorCobrar.Any(x => x.Saldo < 0))
                        //{
                        //    throw new Exception("Para entregar este pedido primero debe saldar los pagos anticipados");
                        //}
                    }

                    //Vemos si esta cancelado en su totalidad
                    //var valoraPagado = salida.CuentaPorCobrarFromIdSalida.Where(x => x.CurrentState == EntityStatesEnum.Loaded).Sum(y => y.Valor);

                    ////Eliminamos el primer pago de contado de un recibo anterior
                    //if ((valoraPagado<salida.Total) && salida.CuentaPorCobrarFromIdSalida.Count() > 0)
                    //{
                    //    salida.CuentaPorCobrarFromIdSalida.RemoveAll(x => x.Numero == 0 && x.CurrentState == EntityStatesEnum.Loaded);
                    //    salida.ReciboCobro.DetalleCobroFromIdCobroDebito = new DetalleCobroEntityCollection(); ;

                    //    //Obtengo los nuevos metodos de pago para distrubuirlos en las cuotas anteriores
                    //    DetalleCobroEntityCollection detCobroList = new DetalleCobroEntityCollection();
                    //    DetalleCobroEntity detCobro;

                    //    foreach (var cxc in salida.CuentaPorCobrarFromIdSalida.Where(x => x.CurrentState == EntityStatesEnum.New))
                    //    {
                    //        foreach (var det in cxc.DetalleCobroFromIdCuentaPorCobrar)
                    //        {
                    //            detCobro = new DetalleCobroEntity { IdMedioPago = det.IdMedioPago, Valor = det.Valor };
                    //            detCobroList.Add(detCobro);
                    //        }
                    //    }

                    //    if (detCobroList.Count > 0)
                    //    {
                    //        foreach (var meth in detCobroList)
                    //        {
                    //            while (meth.Valor > 0)
                    //            {
                    //                foreach (var cxc in salida.CuentaPorCobrarFromIdSalida.Where(x => x.CurrentState != EntityStatesEnum.New && x.Saldo > 0.0M))
                    //                {
                    //                    decimal valMeth = 0;
                    //                    if (meth.Valor > 0)
                    //                    {
                    //                        if (cxc.Saldo >= meth.Valor)
                    //                        {
                    //                            valMeth = meth.Valor;
                    //                            cxc.Saldo = cxc.Saldo - meth.Valor;
                    //                            meth.Valor = 0;
                    //                        }
                    //                        else
                    //                        {
                    //                            valMeth = cxc.Saldo;
                    //                            meth.Valor = meth.Valor - cxc.Saldo;
                    //                            cxc.Saldo = 0;
                    //                        }

                    //                        // Hay que crear un detalle para sustentar el pago.
                    //                        var detalle = new DetalleCobroEntity
                    //                        {
                    //                            IdCobroDebito = salida.ReciboCobro.Id,
                    //                            IdCobroDebitoAsCobroDebito = salida.ReciboCobro,
                    //                            IdCuentaPorCobrar = cxc.Id,
                    //                            IdCuentaPorCobrarAsCuentaPorCobrar = cxc,
                    //                            IdEstado = 1,

                    //                            IdMedioPago = meth.IdMedioPago,
                    //                            Valor = valMeth,
                    //                            Descripcion = meth.Descripcion,

                    //                            FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso,
                    //                            UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso,
                    //                            IpIngreso = salida.IpModificacion ?? salida.IpIngreso
                    //                        };

                    //                        detalle.SetNewState();
                    //                        cxc.DetalleCobroFromIdCuentaPorCobrar.Add(detalle);
                    //                        salida.ReciboCobro.DetalleCobroFromIdCobroDebito.Add(detalle);
                    //                    }

                    //                    //Lo enviamos a Loaded 
                    //                    //cxc.CurrentState = EntityStatesEnum.Loaded;
                    //                }
                    //                if ((salida.CuentaPorCobrarFromIdSalida.Where(x => x.CurrentState == EntityStatesEnum.Loaded).Sum(y => y.Saldo)) <= 0)
                    //                    break;
                    //            }
                    //        }
                    //        //Elimino la nueva cuenta por cobrar
                    //        salida.CuentaPorCobrarFromIdSalida.RemoveAll(x => x.Numero == 0 && x.CurrentState == EntityStatesEnum.New);
                    //    }
                    //}

                    //Generamos Nota de Credito
                    //if ((valoraPagado > salida.Total) && model.GeneraNc)
                    //{
                    //    var cuentas = new CuentaPorCobrarEntity
                    //    {
                    //        IdEstado = 1,
                    //        IdSalida = salida.Id,
                    //        FechaVencimiento = DateTime.Today,
                    //        Numero = salida.CuentaPorCobrarFromIdSalida.Where(x => x.CurrentState == EntityStatesEnum.New).Count(),
                    //        Valor = salida.Total - valoraPagado,
                    //        FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso,
                    //        UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso,
                    //        IpIngreso = salida.IpModificacion ?? salida.IpIngreso
                    //    };

                    //    cuentas.SetNewState();
                    //    salida.CuentaPorCobrarFromIdSalida.Add(cuentas);
                    //}

                    //var salidaJson = salida.ToJson();

                    var result = GuardarSalida(salida)
                            .ToOutputDto();



                    if (!string.IsNullOrEmpty(salida.TransaccionPadre))
                    {
                        var entrada = EntradaCollectionBussinesAction
                            .FindByAll(new EntradaFindParameterEntity
                            {
                                IdEstado = 1,
                                TransaccionPadre = salida.TransaccionPadre
                            })?.FirstOrDefault();

                        if (entrada != null)
                        {
                            entrada.IpModificacion = salida.IpModificacion ?? salida.IpIngreso;
                            entrada.FechaModificacion = salida.FechaModificacion ?? salida.FechaIngreso;
                            entrada.UsuarioModificacion = salida.UsuarioModificacion ?? salida.UsuarioIngreso;
                            entrada.EstadoActual = "APROBADA";

                            entrada.SetUpdatedState();

                            result.Entrada = EntradaBussinesAction
                                .Save(entrada)?
                                .ToInputDto();
                        }
                    }


                    return result;
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


        [HttpPost, Route("salesOrder/{id}/invoice")]
        public OutputDto InvoiceOutput(string id, SalesOrderInvoiceDto model)
        {
            try
            {
                var salida = GetSalidaEntity(id, model);

                if (salida != null)
                {
                    salida.Ruc = model.Ruc;
                    salida.IdCliente = model.IdCliente;
                    salida.FechaHoraEntregaReal = DateTime.Now;
                    salida.EstadoActual = "FACTURADA";
                    salida.SetUpdatedState();

                    if (model.Factura != null && model.Factura.Id == null)
                    {
                        salida.DocumentoFromIdSalida = new DocumentoEntityCollection
                        {
                            model.Factura.ToDocumentoEntity()
                        };
                    }
                    else
                    {
                        throw new Exception("Debe incluir la información de la factura!");
                    }

                    //// Ahora Configuramos la factura:
                    //if (model.Factura != null && model.Factura.Detalles != null && model.Factura.Detalles.Count > 0)
                    //{
                    //    var factura = model.Factura.GenerateInvoice();
                    //    factura.Fecha = DateTime.Today;
                    //    factura.IdMoneda = 1;
                    //    factura.IdEmpresa = salida.IdEmpresa;
                    //    factura.SetNewState();
                    //    factura.DetalleDocumentoFromIdDocumento?.ForEach(x => x.SetNewState());

                    //    salida.DocumentoFromIdSalida = new DocumentoEntityCollection() { factura };
                    //}
                    //else
                    //{
                    //    throw new Exception("Hubo un error de datos de la factura!");
                    //}
                    // Configuro el metodo que genera las facturas electrónicas
                    if (salida.DocumentoFromIdSalida != null && salida.DocumentoFromIdSalida.Count != 0)
                    {
                        var item = salida.DocumentoFromIdSalida.FirstOrDefault();

                        //Quitar esto cuando el oso actualice la app
                        item.Establecimiento = null;
                        item.PuntoEmision = null;
                        //Hasta aqui

                        var LocalBodega = LocalBodegaBussinesAction.LoadByPK(salida.IdLocal);
                        if (LocalBodega.Establecimiento != null && LocalBodega.PuntoEmision != null)
                        {
                            item.Establecimiento = LocalBodega.Establecimiento;
                            item.PuntoEmision = LocalBodega.PuntoEmision;
                        }

                        salida.DocumentoFromIdSalida = new DocumentoEntityCollection { item };
                    }

                    salida.ProcesarFacturaHandler = OutputExtensions.GenerarFacturas;
                    var result = GuardarSalida(salida).ToOutputDto();

                    return result;
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

        [HttpPost, Route("salesOrder/{id}/credit")]
        public OutputDto CreditOutput(string id, SalesOrderInvoiceDto model)
        {
            try
            {
                var salida = GetSalidaEntity(id, model);

                if (salida != null)
                {
                    salida.Ruc = model.Ruc;
                    salida.IdCliente = model.IdCliente;
                    salida.FechaHoraEntregaReal = DateTime.Now;
                    salida.EstadoActual = "FACTURADA";
                    salida.SetUpdatedState();
                    model.Factura.Id = null;

                    if (model.Factura != null && model.Factura.Id == null)
                    {
                        salida.DocumentoFromIdSalida = new DocumentoEntityCollection
                        {
                            model.Factura.ToDocumentoEntity()
                        };
                    }
                    else
                    {
                        throw new Exception("Debe incluir la información de la factura!");
                    }

                    //// Ahora Configuramos la factura:
                    // Configuro el metodo que genera las facturas electrónicas
                    if (salida.DocumentoFromIdSalida != null && salida.DocumentoFromIdSalida.Count != 0)
                    {
                        foreach (var item in salida.DocumentoFromIdSalida)
                        {
                            //Quitar esto cuando el oso actualice la app
                            item.Establecimiento = null;
                            item.PuntoEmision = null;
                            //Hasta aqui

                            var LocalBodega = LocalBodegaBussinesAction.LoadByPK(salida.IdLocal);
                            if (LocalBodega.Establecimiento != null && LocalBodega.PuntoEmision != null)
                            {
                                item.Establecimiento = LocalBodega.Establecimiento;
                                item.PuntoEmision = LocalBodega.PuntoEmision;
                            }
                        }
                    }

                    salida.ProcesarFacturaHandler = OutputExtensions.GenerarNotasCreditos;
                    var result = GuardarSalida(salida).ToOutputDto();

                    return result;
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

        public static SalidaEntity GuardarSalida(SalidaEntity salida)
        {
            SqlConnection connection = null; SqlTransaction transaction = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]))
                {
                    connection.Open();

                    using (transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                    {
                        //Actualizamos ahora toda la Nota Pedido
                        salida = SalidaBussinesAction.Guardar(salida, connection, transaction);

                        transaction.Commit();
                        return salida;
                    }
                }
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }

                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

        }

        static CobroDebitoEntity CrearRecibo(SalidaEntity salida, string motivo)
        {
            var recibo = new CobroDebitoEntity
            {
                IdCliente = salida.IdCliente,
                Fecha = salida.FechaModificacion ?? salida.FechaIngreso,
                Detalle = motivo,
                IdEmpresa = salida.IdEmpresa,
                IdEstado = 1,
                UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso,
                IpIngreso = salida.IpModificacion ?? salida.IpModificacion,
                FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso
            };

            recibo.SetNewState();
            recibo.DetalleCobroFromIdCobroDebito = new DetalleCobroEntityCollection();
            recibo.DetalleMedioCobroFromIdCobroDebito = new DetalleMedioCobroEntityCollection();
            return recibo;
        }

        static async Task SetCuentasPorCobrar(SalidaEntity salida, List<ReceivableDto> cuentasPorCobrar, bool? etapa)
        {
            try
            {
                var cuentasJson = cuentasPorCobrar.ToJson();

                CuentaPorCobrarEntityCollection cuentaPorCobrarFromIdSalida = new CuentaPorCobrarEntityCollection();

                if (salida.CuentaPorCobrarFromIdSalida == null)
                {
                    cuentaPorCobrarFromIdSalida = GetCuentaPorCobrarEntity(salida.Id) ?? new CuentaPorCobrarEntityCollection();
                }

                //valido que hayan actualizado la tabla de los pagos 
                var cuotaValidar = cuentasPorCobrar.FirstOrDefault();
                var recibo = cuentaPorCobrarFromIdSalida.OrderBy(x => x.Numero).FirstOrDefault();
                if (cuotaValidar != null && cuotaValidar.Valor != cuotaValidar.ValorPagado)
                {
                    var montoValidar = cuotaValidar.MetodosPago.Sum(x => x.Valor);

                    if (recibo != null)
                    {
                        var reciboValor = recibo.DetalleCobroFromIdCuentaPorCobrar.Where(x => x.IdCobroDebito != null).Sum(v => v.Valor);
                        //if (reciboValor != montoValidar && salida.EstadoActual != "APROBADA")//no considero el proceso de Aprobar
                        if (salida.EstadoActual == "ENTREGADA")//solo considero el proceso de Entregar
                        {
                            if (salida.Entrega != "INMEDIATA")
                            {
                                montoValidar += reciboValor;
                            }
                        }
                    }

                    if (cuotaValidar.ValorPagado != montoValidar)
                    {
                        if (!cuotaValidar.MetodosPago.Any() && salida.EstadoActual != "ENTREGADA")//ignoro el estado de Entrega por las NC y porque el front si actualiza la tabla
                        {
                            throw new Exception("Debe actualizar la tabla de pagos.");
                        }

                        if (salida.EstadoActual == "ENTREGADA" && montoValidar < cuotaValidar.ValorPagado)
                        {
                            throw new Exception("Error. Actualizar la tabla de pagos o reingresar los métodos de pago.");
                        }

                        //if (salida.EstadoActual == "ENTREGADA" && cuotaValidar.ValorPagado != montoValidar && cuentasPorCobrar.Count() > 1)
                        if (cuotaValidar.ValorPagado != montoValidar && cuentasPorCobrar.Count() > 1)
                        {
                            throw new Exception("Debe actualizar la tabla de pagos.");
                        }
                        else if (cuotaValidar.ValorPagado != cuotaValidar.Valor)
                        {
                            throw new Exception("Debe actualizar la tabla de pagos.");
                        }
                    }

                }

                await SetCuentasPorCobrarCarmita(salida, cuentasPorCobrar);
            }
            catch (Exception ex)
            {
                if (ex is HttpResponseException)
                {
                    throw ex;
                }
                else
                {
                    var Request = new HttpRequestMessage();
                    throw Request.BuildExceptionResponse(ex);
                }
            }
        }

        static async Task SetCuentasPorCobrarAprobar(SalidaEntity salida, List<ReceivableDto> cuentasPorCobrar, bool? etapa)
        {
            try
            {
                var cuentasJson = cuentasPorCobrar.ToJson();

                CuentaPorCobrarEntityCollection cuentaPorCobrarFromIdSalida = new CuentaPorCobrarEntityCollection();

                if (salida.CuentaPorCobrarFromIdSalida == null)
                {
                    cuentaPorCobrarFromIdSalida = GetCuentaPorCobrarEntity(salida.Id) ?? new CuentaPorCobrarEntityCollection();
                }

                //valido que hayan actualizado la tabla de los pagos 
                var cuotaValidar = cuentasPorCobrar.FirstOrDefault();
                var recibo = cuentaPorCobrarFromIdSalida.OrderBy(x => x.Numero).FirstOrDefault();
                if (cuotaValidar != null)
                {
                    var montoValidar = cuotaValidar.MetodosPago.Sum(x => x.Valor);

                    if (recibo != null)
                    {
                        var reciboValor = recibo.DetalleCobroFromIdCuentaPorCobrar.Where(x => x.IdCobroDebito != null).Sum(v => v.Valor);
                        //if (reciboValor != montoValidar && salida.EstadoActual != "APROBADA")//no considero el proceso de Aprobar
                        if (salida.EstadoActual == "ENTREGADA")//solo considero el proceso de Entregar
                        {
                            if (salida.Entrega != "INMEDIATA")
                            {
                                montoValidar += reciboValor;
                            }
                        }
                    }

                    if (cuotaValidar.ValorPagado != montoValidar)
                    {
                        if (!cuotaValidar.MetodosPago.Any() && salida.EstadoActual != "ENTREGADA")//ignoro el estado de Entrega por las NC y porque el front si actualiza la tabla
                        {
                            throw new Exception("Debe actualizar la tabla de pagos.");
                        }

                        if (salida.EstadoActual == "ENTREGADA" && montoValidar < cuotaValidar.ValorPagado)
                        {
                            throw new Exception("Error. Actualizar la tabla de pagos o reingresar los métodos de pago.");
                        }

                        //if (salida.EstadoActual == "ENTREGADA" && cuotaValidar.ValorPagado != montoValidar && cuentasPorCobrar.Count() > 1)
                        if (cuotaValidar.ValorPagado != montoValidar && cuentasPorCobrar.Count() > 1)
                        {
                            throw new Exception("Debe actualizar la tabla de pagos.");
                        }
                        else if (cuotaValidar.ValorPagado != cuotaValidar.Valor)
                        {
                            throw new Exception("Debe actualizar la tabla de pagos.");
                        }
                    }

                }

                await SetCuentasPorCobrarAprobarCarmita(salida, cuentasPorCobrar);
            }
            catch (Exception ex)
            {
                if (ex is HttpResponseException)
                {
                    throw ex;
                }
                else
                {
                    var Request = new HttpRequestMessage();
                    throw Request.BuildExceptionResponse(ex);
                }
            }
        }

        static async Task SetEntregaCuentasPorCobrar(SalidaEntity salida, List<ReceivableDto> cuentasPorCobrar, bool? etapa)
        {
            try
            {
                var cuentasJson = cuentasPorCobrar.ToJson();

                CuentaPorCobrarEntityCollection cuentaPorCobrarFromIdSalida = new CuentaPorCobrarEntityCollection();

                if (salida.CuentaPorCobrarFromIdSalida == null)
                {
                    cuentaPorCobrarFromIdSalida = GetCuentaPorCobrarEntity(salida.Id) ?? new CuentaPorCobrarEntityCollection();
                }

                //valido que hayan actualizado la tabla de los pagos 
                var cuotaValidar = cuentasPorCobrar.FirstOrDefault();
                var recibo = cuentaPorCobrarFromIdSalida.OrderBy(x => x.Numero).FirstOrDefault();
                if (cuotaValidar != null && cuotaValidar.Valor != cuotaValidar.ValorPagado)
                {
                    var montoValidar = cuotaValidar.MetodosPago.Sum(x => x.Valor);

                    if (recibo != null)
                    {
                        var reciboValor = recibo.DetalleCobroFromIdCuentaPorCobrar.Where(x => x.IdCobroDebito != null).Sum(v => v.Valor);
                        //if (reciboValor != montoValidar && salida.EstadoActual != "APROBADA")//no considero el proceso de Aprobar
                        if (salida.EstadoActual == "ENTREGADA")//solo considero el proceso de Entregar
                        {
                            if (salida.Entrega != "INMEDIATA")
                            {
                                montoValidar += reciboValor;
                            }
                        }
                    }

                    if (cuotaValidar.ValorPagado != montoValidar)
                    {
                        if (!cuotaValidar.MetodosPago.Any() && salida.EstadoActual != "ENTREGADA")//ignoro el estado de Entrega por las NC y porque el front si actualiza la tabla
                        {
                            throw new Exception("Debe actualizar la tabla de pagos.");
                        }

                        if (salida.EstadoActual == "ENTREGADA" && montoValidar < cuotaValidar.ValorPagado)
                        {
                            throw new Exception("Error. Actualizar la tabla de pagos o reingresar los métodos de pago.");
                        }

                        //if (salida.EstadoActual == "ENTREGADA" && cuotaValidar.ValorPagado != montoValidar && cuentasPorCobrar.Count() > 1)
                        if (cuotaValidar.ValorPagado != montoValidar && cuentasPorCobrar.Count() > 1)
                        {
                            throw new Exception("Debe actualizar la tabla de pagos.");
                        }
                        else if (cuotaValidar.ValorPagado != cuotaValidar.Valor)
                        {
                            throw new Exception("Debe actualizar la tabla de pagos.");
                        }
                    }

                }

                await SetCuentasPorCobrarEntregaCarmita(salida, cuentasPorCobrar);
            }
            catch (Exception ex)
            {
                if (ex is HttpResponseException)
                {
                    throw ex;
                }
                else
                {
                    var Request = new HttpRequestMessage();
                    throw Request.BuildExceptionResponse(ex);
                }
            }
        }

        static async Task SetCuentasPorCobrarCarmita(SalidaEntity salida, List<ReceivableDto> cuentasPorCobrar)
        {
            try
            {
                var cliente = new HttpClient();
                cliente.BaseAddress = new Uri("https://tendago-salida-aps.azurewebsites.net/");

                var jsonContent = new StringContent(cuentasPorCobrar.ToJson(), Encoding.UTF8, "application/json");

                var response = await cliente.PostAsync($"api/cuentaporcobrar/crear/{salida.Id}?usuario={salida.UsuarioModificacion ?? salida.UsuarioIngreso}", jsonContent);

                if (response.StatusCode != System.Net.HttpStatusCode.OK) throw new Exception("Error en la cuenta.");
            }
            catch (Exception)
            {

                throw;
            }
        }

        static async Task SetCuentasPorCobrarAprobarCarmita(SalidaEntity salida, List<ReceivableDto> cuentasPorCobrar)
        {
            try
            {
                var cliente = new HttpClient();
                cliente.BaseAddress = new Uri("https://tendago-salida-aps.azurewebsites.net/");

                var cuentasPorCobrarJson = cuentasPorCobrar.ToJson();

                var jsonContent = new StringContent(cuentasPorCobrar.ToJson(), Encoding.UTF8, "application/json");

                var response = await cliente.PostAsync($"api/cuentaporcobrar/aprobar/{salida.Id}?usuario={salida.UsuarioModificacion ?? salida.UsuarioIngreso}", jsonContent);

                if (response.StatusCode != System.Net.HttpStatusCode.OK) throw new Exception("Error en la cuenta.");
            }
            catch (Exception)
            {

                throw;
            }
        }

        static async Task SetCuentasPorCobrarEntregaCarmita(SalidaEntity salida, List<ReceivableDto> cuentasPorCobrar)
        {
            try
            {
                var cliente = new HttpClient();
                cliente.BaseAddress = new Uri("https://tendago-salida-aps.azurewebsites.net/");

                var cuentasPorCobrarJson = cuentasPorCobrar.ToJson();

                var jsonContent = new StringContent(cuentasPorCobrar.ToJson(), Encoding.UTF8, "application/json");

                var response = await cliente.PostAsync($"api/cuentaporcobrar/entregar/{salida.Id}?usuario={salida.UsuarioModificacion ?? salida.UsuarioIngreso}", jsonContent);

                if (response.StatusCode != System.Net.HttpStatusCode.OK) throw new Exception("Error en la cuenta.");
            }
            catch (Exception)
            {

                throw;
            }
        }

        static void SetCuentasPorCobrarEntrega(SalidaEntity salida, List<ReceivableDto> cuentasPorCobrar, bool? etapa)
        {
            try
            {

                var cuentasJson = cuentasPorCobrar.ToJson();

                List<ReceivableDto> cxcs = new List<ReceivableDto>();

                if (salida.CuentaPorCobrarFromIdSalida == null)
                {
                    salida.CuentaPorCobrarFromIdSalida = GetCuentaPorCobrarEntity(salida.Id) ?? new CuentaPorCobrarEntityCollection();
                }

                //valido que hayan actualizado la tabla de los pagos 
                var cuotaValidar = cuentasPorCobrar.FirstOrDefault();
                var recibo = salida.CuentaPorCobrarFromIdSalida.OrderBy(x => x.Numero).FirstOrDefault();
                if (cuotaValidar != null)
                {
                    var montoValidar = cuotaValidar.MetodosPago.Sum(x => x.Valor);

                    if (recibo != null)
                    {
                        var reciboValor = recibo.DetalleCobroFromIdCuentaPorCobrar.Where(x => x.IdCobroDebito != null).Sum(v => v.Valor);
                        //if (reciboValor != montoValidar && salida.EstadoActual != "APROBADA")//no considero el proceso de Aprobar
                        if (salida.EstadoActual == "ENTREGADA")//solo considero el proceso de Entregar
                        {
                            if (salida.Entrega != "INMEDIATA")
                            {
                                montoValidar += reciboValor;
                            }
                        }
                    }

                    if (cuotaValidar.ValorPagado != montoValidar)
                    {
                        if (!cuotaValidar.MetodosPago.Any() && salida.EstadoActual != "ENTREGADA")//ignoro el estado de Entrega por las NC y porque el front si actualiza la tabla
                        {
                            throw new Exception("Debe actualizar la tabla de pagos.");
                        }

                        if (salida.EstadoActual == "ENTREGADA" && montoValidar < cuotaValidar.ValorPagado)
                        {
                            throw new Exception("Error. Actualizar la tabla de pagos o reingresar los métodos de pago.");
                        }

                        //if (salida.EstadoActual == "ENTREGADA" && cuotaValidar.ValorPagado != montoValidar && cuentasPorCobrar.Count() > 1)
                        if (cuotaValidar.ValorPagado != montoValidar && cuentasPorCobrar.Count() > 1)
                        {
                            throw new Exception("Debe actualizar la tabla de pagos.");
                        }
                        else if (cuotaValidar.ValorPagado != cuotaValidar.Valor)
                        {
                            throw new Exception("Debe actualizar la tabla de pagos.");
                        }
                    }

                }

                // Primero Busco las cuentas por cobrar eliminadas para quitarlas
                if (cuentasPorCobrar.Count != 0)
                {
                    //verifico si tiene pagos por medio del recibo y asi guardarlos al entregar
                    var pagosRealizados = recibo.DetalleCobroFromIdCuentaPorCobrar.Sum(x => x.Valor);
                    var pagosPorGuardar = cuentasPorCobrar.Sum(x => x.MetodosPago.Sum(p => p.Valor));

                    foreach (var cxc in salida.CuentaPorCobrarFromIdSalida)
                    {
                        var cxcSId = cuentasPorCobrar.FirstOrDefault(x => x.Id == 0 && cxc.Valor == x.Valor && cxc.Saldo == x.Saldo);
                        if (cxcSId != null)
                        {
                            cxcSId.Id = cxc.Id;
                        }

                        if (!cuentasPorCobrar.Any(m => m.Id != 0 && cxc.Saldo <= 0))
                        {
                            if (!cuentasPorCobrar.Any(m => m.Numero == cxc.Numero && m.Id == cxc.Id))
                            {
                                cxc.IdSalida = salida.Id;
                                cxc.Numero = (cxc.Id * 1000) + cxc.Numero; // Actualizo el numero de la cuenta por cobrar
                                cxc.FechaModificacion = salida.FechaModificacion;
                                cxc.UsuarioModificacion = salida.UsuarioModificacion;
                                cxc.IpModificacion = salida.IpModificacion;
                                cxc.IdEstado = 0;
                                cxc.SetDeletedState();

                                if (cxc.DetalleCobroFromIdCuentaPorCobrar != null)
                                {
                                    foreach (var det in cxc.DetalleCobroFromIdCuentaPorCobrar)
                                    {
                                        //agrego los pagos del recibo para guardar posteriormente
                                        if (pagosPorGuardar != cuotaValidar.ValorPagado && pagosRealizados > 0 && det.Valor > 0)
                                        {
                                            var oldMetodoPago = new ReceivablePayMethodDto()
                                            {
                                                IdMedioPago = det.IdMedioPago,
                                                Valor = det.Valor,
                                                Descripcion = det.Descripcion,
                                                FechaIngreso = det.FechaIngreso,
                                                UsuarioIngreso = det.UsuarioIngreso
                                            };
                                            cuotaValidar.MetodosPago.Add(oldMetodoPago);
                                        }

                                        det.FechaModificacion = salida.FechaModificacion;
                                        det.UsuarioModificacion = salida.UsuarioModificacion;
                                        det.IpModificacion = salida.IpModificacion;
                                        det.IdEstado = 0;
                                        det.SetDeletedState();

                                        //if (det.IdCobroDebitoAsCobroDebito != null)
                                        //{
                                        //    det.IdCobroDebitoAsCobroDebito.FechaModificacion = salida.FechaModificacion;
                                        //    det.IdCobroDebitoAsCobroDebito.UsuarioModificacion = salida.UsuarioModificacion;
                                        //    det.IdCobroDebitoAsCobroDebito.IpModificacion = salida.IpModificacion;
                                        //    det.IdCobroDebitoAsCobroDebito.IdEstado = 0;
                                        //    det.IdCobroDebitoAsCobroDebito.SetDeletedState();
                                        //}
                                    }

                                }
                            }
                        }
                    }
                }


                //foreach (var cxc in cuentasPorCobrar)
                //{
                //    cxcs.Add(cxc);

                //    if (cxc.Saldo < 0)
                //    {
                //        cxcs.Add(new ReceivableDto
                //        {
                //            Id = 0,
                //            IdEstado = 0,
                //            IdMedioPago = 1,
                //            IdSalida = null,
                //            Ip = null,
                //            MetodosPago = new List<ReceivablePayMethodDto>(),
                //            Numero = 0,
                //            NumeroFactura = null,
                //            Saldo = 0,
                //            Usuario = null,
                //            Valor = cxc.Saldo,
                //            ValorPagado = cxc.Saldo,
                //            FechaVencimiento = DateTime.Now
                //        });
                //    }

                //}

                foreach (var cxc in cuentasPorCobrar)
                {

                    var cxcEntity = salida.CuentaPorCobrarFromIdSalida.FirstOrDefault(m => m.Id == cxc.Id && m.Numero == cxc.Numero && m.IdEstado == 1);

                    if (cxcEntity != null)
                    {
                        cxcEntity.UpdateEntity(cxc);
                        cxcEntity.IdEstado = 1;
                        cxcEntity.IdSalida = salida.Id;
                        cxcEntity.FechaModificacion = salida.FechaModificacion;
                        cxcEntity.UsuarioModificacion = salida.UsuarioModificacion;
                        cxcEntity.IpModificacion = salida.IpModificacion;
                        cxcEntity.SetUpdatedState();
                    }
                    else
                    {
                        cxcEntity = cxc.ToReceivableEntity();
                        cxcEntity.IdEstado = 1;
                        cxcEntity.IdSalida = salida.Id;
                        cxcEntity.FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso;
                        cxcEntity.UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso;
                        cxcEntity.IpIngreso = salida.IpModificacion ?? salida.IpIngreso;
                        cxcEntity.Numero = salida.CuentaPorCobrarFromIdSalida.Where(x => x.IdEstado == 1 && x.CurrentState != EntityStatesEnum.Deleted).Count();
                        cxcEntity.SetNewState();

                        salida.CuentaPorCobrarFromIdSalida.Add(cxcEntity);
                    }

                    //verifico si se cambiaron los metodos de pago
                    bool nuevosMetodos = false;
                    if (cxcEntity.DetalleCobroFromIdCuentaPorCobrar != null && cxc.MetodosPago != null)
                    {
                        nuevosMetodos = CompararMetodosPago(cxcEntity.DetalleCobroFromIdCuentaPorCobrar, cxc.MetodosPago);
                    }

                    //elimino el detalleCobro y su recibo para crear uno nuevo con sus nuevos metodos de pago
                    if ((cxc.ValorPagado > 0 && cxc.Saldo != 0) || cxcEntity.CurrentState == EntityStatesEnum.New || nuevosMetodos)
                    {
                        cxcEntity.DetalleCobroFromIdCuentaPorCobrar = cxcEntity.DetalleCobroFromIdCuentaPorCobrar ?? new DetalleCobroEntityCollection();

                        var totalPagos = cxc.MetodosPago.Sum(x => x.Valor);
                        var newPago = salida.CuentaPorCobrarFromIdSalida.FirstOrDefault(x => x.DetalleCobroFromIdCuentaPorCobrar.Count == 1
                                                                        && x.PreviousState == EntityStatesEnum.Updated)
                                                                        ?.DetalleCobroFromIdCuentaPorCobrar.FirstOrDefault();

                        if ((totalPagos != cxc.ValorPagado || nuevosMetodos) && newPago != null)
                        {
                            //eliminamos el detalleCobro por si no se elimino deste la cxc
                            if (cxc.ValorPagado == newPago.Valor && newPago.CurrentState != EntityStatesEnum.Deleted)
                            {
                                newPago.FechaModificacion = cxcEntity.FechaIngreso;
                                newPago.UsuarioModificacion = cxcEntity.UsuarioIngreso;
                                newPago.IpModificacion = cxcEntity.IpIngreso;
                                newPago.IdEstado = 0;
                                newPago.SetDeletedState();

                                //if (newPago.IdCobroDebitoAsCobroDebito != null)
                                //{
                                //    newPago.IdCobroDebitoAsCobroDebito.FechaModificacion = cxcEntity.FechaIngreso;
                                //    newPago.IdCobroDebitoAsCobroDebito.UsuarioModificacion = cxcEntity.UsuarioIngreso;
                                //    newPago.IdCobroDebitoAsCobroDebito.IpModificacion = cxcEntity.IpIngreso;
                                //    newPago.IdCobroDebitoAsCobroDebito.IdEstado = 0;
                                //    newPago.IdCobroDebitoAsCobroDebito.SetDeletedState();
                                //}
                            }

                            if (!nuevosMetodos)
                            {
                                var newMetodoPago = new ReceivablePayMethodDto()
                                {
                                    IdMedioPago = newPago.IdMedioPago,
                                    Valor = newPago.Valor,
                                    Descripcion = newPago.Descripcion
                                };
                                cxc.MetodosPago.Add(newMetodoPago);
                            }
                        }

                        //CobroDebitoEntityCollection cobroDebito = null;
                        //List<CobroDebitoEntity> cobroDebitoLista = new List<CobroDebitoEntity>();

                        //if (cxc.MetodosPago.Count > 0)
                        //{
                        //    cobroDebito = CobroDebitoCollectionBussinesAction.FindByAll(new CobroDebitoFindParameterEntity
                        //    {
                        //        Id = cxc.MetodosPago[0].IdCobroDebito,
                        //        IdEstado = 1,
                        //    }, 0);
                        //    foreach (var item in cobroDebito.Where(x => x.FechaIngreso >= DateTime.Now.AddYears(-1)).ToList())
                        //    {
                        //        cobroDebitoLista.Add(item);
                        //    }
                        //}

                        if (cxc.MetodosPago.Count != 0)
                        {
                            foreach (var metodo in cxc.MetodosPago)
                            {

                                if (salida.ReciboCobro == null)
                                {
                                    CobroDebitoEntregaEntity(salida, null);
                                }

                                if (metodo.Valor > 0)
                                {
                                    // Hay que crear un detalle para sustentar el pago.
                                    var detalle = new DetalleCobroEntity
                                    {
                                        IdCobroDebito = salida.ReciboCobro?.Id,
                                        IdCobroDebitoAsCobroDebito = salida.ReciboCobro,
                                        IdCuentaPorCobrar = cxcEntity.Id,
                                        IdCuentaPorCobrarAsCuentaPorCobrar = cxcEntity,
                                        IdEstado = 1,
                                        IdLocal = salida.IdLocal,

                                        IdMedioPago = metodo.IdMedioPago,
                                        Valor = metodo.Valor,
                                        Descripcion = metodo?.Descripcion,

                                        //FechaIngreso = newPago != null && metodo.Valor == newPago.Valor ? newPago.FechaIngreso : metodo.FechaIngreso ?? salida.FechaModificacion ?? salida.FechaIngreso,
                                        FechaIngreso = newPago != null && metodo.Valor == newPago.Valor ? DateTime.Now : metodo.FechaIngreso == null ? DateTime.Now : newPago != null ? newPago.FechaIngreso : DateTime.Now,
                                        UsuarioIngreso = newPago != null && metodo.Valor == newPago.Valor ? newPago.UsuarioIngreso : metodo.UsuarioIngreso ?? salida.UsuarioModificacion ?? salida.UsuarioIngreso,
                                        IpIngreso = newPago != null && metodo.Valor == newPago.Valor ? newPago.IpIngreso : salida.IpModificacion ?? salida.IpIngreso
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

                                        UsuarioIngreso = newPago != null && metodo.Valor == newPago.Valor ? newPago.UsuarioIngreso : metodo.UsuarioIngreso ?? salida.UsuarioModificacion ?? salida.UsuarioIngreso,
                                        FechaIngreso = newPago != null && metodo.Valor == newPago.Valor ? DateTime.Now : metodo.FechaIngreso == null ? DateTime.Now : newPago != null ? newPago.FechaIngreso : DateTime.Now,
                                        IpIngreso = newPago != null && metodo.Valor == newPago.Valor ? newPago.IpIngreso : salida.IpModificacion ?? salida.IpIngreso,

                                    };

                                    medioCobro.SetNewState();
                                    salida.ReciboCobro.DetalleMedioCobroFromIdCobroDebito.Add(medioCobro);
                                }
                            }
                        }

                        //Añado el negativo 
                        if (cxcEntity.Saldo < 0 && salida.EstadoActual == "ENTREGADA")
                        {
                            CobroDebitoEntity reciboNew = new CobroDebitoEntity
                            {
                                IdCliente = salida.IdCliente,
                                Fecha = salida.FechaModificacion ?? salida.FechaIngreso,
                                IdEmpresa = salida.IdEmpresa,
                                IdEstado = 1,
                                UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso,
                                IpIngreso = salida.IpModificacion ?? salida.IpModificacion,
                                FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso
                            };


                            reciboNew.SetNewState();

                            var detalle = new DetalleCobroEntity
                            {
                                IdCobroDebito = reciboNew.Id,
                                IdCobroDebitoAsCobroDebito = salida.ReciboCobro,
                                IdCuentaPorCobrar = cxcEntity.Id,
                                IdCuentaPorCobrarAsCuentaPorCobrar = cxcEntity,
                                IdEstado = 1,
                                IdLocal = salida.IdLocal,
                                IdMedioPago = cxc.IdMedioPago == 0 ? 1 : cxc.IdMedioPago,
                                Valor = cxc.Saldo,
                                FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso,
                                UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso,
                                IpIngreso = salida.IpModificacion ?? salida.IpIngreso
                            };

                            detalle.SetNewState();

                            cxcEntity.DetalleCobroFromIdCuentaPorCobrar.Add(detalle);
                            salida.ReciboCobro.DetalleCobroFromIdCobroDebito.Add(detalle);

                            var medioCobro = new DetalleMedioCobroEntity
                            {
                                IdCobroDebito = salida.ReciboCobro.Id,
                                IdEstado = 1,
                                IdMedioPago = detalle.IdMedioPago,
                                Valor = detalle.Valor,
                                Descripcion = detalle.Descripcion,

                                UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso,
                                FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso,
                                IpIngreso = salida.IpModificacion ?? salida.IpIngreso,

                            };

                            medioCobro.SetNewState();
                            salida.ReciboCobro.DetalleMedioCobroFromIdCobroDebito.Add(medioCobro);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is HttpResponseException)
                {
                    throw ex;
                }
                else
                {
                    var Request = new HttpRequestMessage();
                    throw Request.BuildExceptionResponse(ex);
                }
            }
        }

        static bool CompararMetodosPago(DetalleCobroEntityCollection lista1, List<ReceivablePayMethodDto> lista2)
        {
            //Verificar si ambas listas tienen pagos, si no, es false
            if (lista1.Any() && lista2.Any())
            {

                // Verificar si las listas tienen la misma longitud
                if (lista1.Count != lista2.Count)
                {
                    return true;
                }

                // Comparar elementos de las listas
                for (int i = 0; i < lista1.Count; i++)
                {
                    if (lista1[i].IdMedioPago != lista2[i].IdMedioPago || lista1[i].Valor != lista2[i].Valor)
                    {
                        return true;
                    }
                }
            }

            // Si no se encontraron diferencias, las listas son iguales o si una no tien
            return false;
        }

        public static void SetDetalles(SalidaEntity salida, List<SalesOrderDetailDto> detalles)
        {
            if (detalles.Count == 0)
            {
                throw new Exception("No se puede eliminar todos los productos, en su lugar debe anular la Orden");
            }
            if (salida.DetalleSalidaFromIdSalida == null)
            {
                salida.DetalleSalidaFromIdSalida = GetDetallesEntity(salida.Id) ?? new DetalleSalidaEntityCollection();
            }

            if (detalles.Count > 0)
            {
                var deleted = salida.DetalleSalidaFromIdSalida.Where(detalle => !detalles.Any(detail => detail.Id == detalle.Id));

                // Este es un proceso para validar que se esta eliminando los detalles correctos. 
                // De lo contrario no los elimina hasta corregir el tema de los pedidos grandes.

                var del = deleted.Count();
                var cant = detalles.Count;
                var last = salida.DetalleSalidaFromIdSalida.Count;
                var rest = last - cant;

                // Los detalles eliminados deben coincidir con 
                // la cantidad de detalles faltantes
                if (rest == del)
                {
                    foreach (var item in deleted)
                    {
                        item.IdEstado = 0;
                        item.FechaModificacion = salida.FechaModificacion;
                        item.UsuarioModificacion = salida.UsuarioModificacion;
                        item.IpModificacion = salida.IpModificacion;
                        item.SetDeletedState();
                    }
                }

                foreach (var detalle in detalles)
                {
                    var detalleEntity = salida.DetalleSalidaFromIdSalida.FirstOrDefault(m => m.Id == detalle.Id);

                    if (detalleEntity != null)
                    {
                        // Actualizo los detalles con los datos enviados
                        detalleEntity.UpdateEntity(detalle);
                        detalleEntity.IdSalida = salida.Id;
                        detalleEntity.IdEstado = 1;
                        detalleEntity.Fecha = salida.Fecha;
                        detalleEntity.FechaModificacion = salida.FechaModificacion;
                        detalleEntity.UsuarioModificacion = salida.UsuarioModificacion;
                        detalleEntity.IpModificacion = salida.IpModificacion;
                        detalleEntity.SetUpdatedState();
                    }
                    else
                    {
                        detalleEntity = detalle.ToDetalleSalidaEntity();
                        detalleEntity.IdSalida = salida.Id;
                        detalleEntity.IdEstado = 1;
                        detalleEntity.Fecha = salida.Fecha;
                        detalleEntity.FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso;
                        detalleEntity.UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso;
                        detalleEntity.IpIngreso = salida.IpModificacion ?? salida.IpIngreso;
                        detalleEntity.SetNewState();

                        salida.DetalleSalidaFromIdSalida.Add(detalleEntity);
                    }
                }
            }
        }

        public static void SetEmpaquetado(SalidaEntity salida, List<PackingDto> empaques)
        {
            salida.EmpaquetadoFromIdSalida = salida.EmpaquetadoFromIdSalida ?? GetEmpaquetadoEntity(salida.Id);

            var packingList = new EmpaquetadoEntityCollection();

            foreach (var paquete in empaques)
            {
                var paqueteEntity = salida.EmpaquetadoFromIdSalida?.FirstOrDefault(m => m.Id == paquete.Id);

                if (paqueteEntity != null)
                {
                    // Actualizo los detalles con los datos enviados
                    paqueteEntity.UpdateEntity(paquete);
                    paqueteEntity.IdSalida = salida.Id;
                    paqueteEntity.IdEstado = 1;
                    paqueteEntity.FechaModificacion = salida.FechaModificacion;
                    paqueteEntity.UsuarioModificacion = salida.UsuarioModificacion;
                    paqueteEntity.IpModificacion = salida.IpModificacion;
                    paqueteEntity.SetUpdatedState();
                }
                else
                {
                    paqueteEntity = paquete.ToEmpaquetadoEntity();
                    paqueteEntity.IdSalida = salida.Id;
                    paqueteEntity.IdEstado = 1;
                    paqueteEntity.FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso;
                    paqueteEntity.UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso;
                    paqueteEntity.IpIngreso = salida.IpModificacion ?? salida.IpIngreso;
                    paqueteEntity.SetNewState();
                }

                packingList.Add(paqueteEntity);
            }

            salida.EmpaquetadoFromIdSalida = packingList;
        }

        public static SalidaEntity GetSalidaEntity(string idSalida, SalesOrderDto model)
        {
            var salida = SalidaBussinesAction.LoadByPK(idSalida, 1);

            if (salida != null)
            {
                // Actualizamos la entidad:
                salida.IdEstado = 1;
                salida.FechaModificacion = DateTime.Now;
                salida.UsuarioModificacion = model.Usuario;
                salida.IpModificacion = model.Ip;
            }

            return salida;
        }

        static DetalleSalidaEntityCollection GetDetallesEntity(string idSalida)
        {
            return DetalleSalidaCollectionBussinesAction.FindByAll(new DetalleSalidaFindParameterEntity { IdSalida = idSalida, IdEstado = 1 }, 0); ;
        }

        static EmpaquetadoEntityCollection GetEmpaquetadoEntity(string idSalida)
        {
            return EmpaquetadoCollectionBussinesAction.FindByAll(new EmpaquetadoFindParameterEntity { IdSalida = idSalida, IdEstado = 1 }, 0); ;
        }

        static CuentaPorCobrarEntityCollection GetCuentaPorCobrarEntity(string idSalida)
        {
            var cuentasPorCobrar = CuentaPorCobrarCollectionBussinesAction.FindByAll(new CuentaPorCobrarFindParameterEntity { IdSalida = idSalida, IdEstado = 1 }, 0); ;

            foreach (var item in cuentasPorCobrar)
            {
                item.DetalleCobroFromIdCuentaPorCobrar = DetalleCobroCollectionBussinesAction.FindByAll(new DetalleCobroFindParameterEntity { IdCuentaPorCobrar = item.Id, IdEstado = 1 }, 1);
            }

            return cuentasPorCobrar;
        }

        static void CobroDebitoEntity(SalidaEntity salida, CobroDebitoEntity cobroDebito = null)
        {
            CobroDebitoEntity recibo;
            if (cobroDebito == null)
            {
                recibo = new CobroDebitoEntity
                {
                    IdCliente = salida.IdCliente,
                    Fecha = salida.FechaModificacion ?? salida.FechaIngreso,
                    IdEmpresa = salida.IdEmpresa,
                    IdEstado = 1,
                    UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso,
                    IpIngreso = salida.IpModificacion ?? salida.IpModificacion,
                    FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso
                };


                recibo.SetNewState();
            }
            else
            {
                recibo = cobroDebito;
                recibo.UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso;
                recibo.IpIngreso = salida.IpModificacion ?? salida.IpModificacion;
                recibo.FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso;
            }

            recibo.DetalleCobroFromIdCobroDebito = new DetalleCobroEntityCollection();
            recibo.DetalleMedioCobroFromIdCobroDebito = new DetalleMedioCobroEntityCollection();

            salida.ReciboCobro = recibo;

            foreach (var cxc in salida.CuentaPorCobrarFromIdSalida)
            {
                foreach (var dcxc in cxc.DetalleCobroFromIdCuentaPorCobrar)
                {
                    //var cuentasPorCobrar = DetalleMedioCobroCollectionBussinesAction.FindByAll(new DetalleMedioCobroFindParameterEntity { IdCobroDebito = dcxc.IdCobroDebito, IdEstado = 1 }, 0); ;
                    salida.ReciboCobro.Id = dcxc.IdCobroDebito;
                    salida.ReciboCobro.UsuarioModificacion = salida.UsuarioIngreso;
                    salida.ReciboCobro.FechaModificacion = DateTime.Now;
                    salida.ReciboCobro.IpModificacion = "::1";

                    //foreach (var item in cuentasPorCobrar)
                    //{

                    //    var medioCobro = new DetalleMedioCobroEntity
                    //    {
                    //        Id = item.Id,
                    //        FechaModificacion = salida.FechaModificacion,
                    //        UsuarioModificacion = salida.UsuarioModificacion,
                    //        IpModificacion = salida.IpModificacion,
                    //        IdEstado = 0,
                    //    };

                    //    medioCobro.SetDeletedState();
                    //    salida.ReciboCobro.DetalleMedioCobroFromIdCobroDebito.Add(medioCobro);
                    //}
                }
            }

        }

        static void CobroDebitoEntregaEntity(SalidaEntity salida, CobroDebitoEntity cobroDebito = null)
        {
            CobroDebitoEntity recibo;
            if (cobroDebito == null)
            {
                recibo = new CobroDebitoEntity
                {
                    IdCliente = salida.IdCliente,
                    Fecha = salida.FechaModificacion ?? salida.FechaIngreso,
                    IdEmpresa = salida.IdEmpresa,
                    IdEstado = 1,
                    UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso,
                    IpIngreso = salida.IpModificacion ?? salida.IpModificacion,
                    FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso
                };


                recibo.SetNewState();
            }
            else
            {
                recibo = cobroDebito;
                recibo.UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso;
                recibo.IpIngreso = salida.IpModificacion ?? salida.IpModificacion;
                recibo.FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso;
            }

            recibo.DetalleCobroFromIdCobroDebito = new DetalleCobroEntityCollection();
            recibo.DetalleMedioCobroFromIdCobroDebito = new DetalleMedioCobroEntityCollection();

            salida.ReciboCobro = recibo;

            foreach (var cxc in salida.CuentaPorCobrarFromIdSalida)
            {
                foreach (var dcxc in cxc.DetalleCobroFromIdCuentaPorCobrar)
                {
                    //var cuentasPorCobrar = DetalleMedioCobroCollectionBussinesAction.FindByAll(new DetalleMedioCobroFindParameterEntity { IdCobroDebito = dcxc.IdCobroDebito, IdEstado = 1 }, 0); ;
                    salida.ReciboCobro.Id = dcxc.IdCobroDebito;
                    salida.ReciboCobro.UsuarioModificacion = salida.UsuarioIngreso;
                    salida.ReciboCobro.FechaModificacion = DateTime.Now;
                    salida.ReciboCobro.IpModificacion = "::1";

                    //foreach (var item in cuentasPorCobrar)
                    //{

                    //    var medioCobro = new DetalleMedioCobroEntity
                    //    {
                    //        Id = item.Id,
                    //        FechaModificacion = salida.FechaModificacion,
                    //        UsuarioModificacion = salida.UsuarioModificacion,
                    //        IpModificacion = salida.IpModificacion,
                    //        IdEstado = 0,
                    //    };

                    //    medioCobro.SetDeletedState();
                    //    salida.ReciboCobro.DetalleMedioCobroFromIdCobroDebito.Add(medioCobro);
                    //}
                }
            }

        }
    }
}