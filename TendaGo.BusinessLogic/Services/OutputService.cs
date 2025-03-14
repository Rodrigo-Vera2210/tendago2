using ER.BE;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TendaGo.Common;

namespace TendaGo.BusinessLogic.Services
{
    public class OutputService
    {
        //public List<OutputDto> GetOutputs()
        //{
        //    var outputs = SalidaCollectionBussinesAction.FindByAll(new SalidaFindParameterEntity { IdEmpresa = CurrentUser.IdEmpresa }, 0);

        //    // Cargo la lista de clientes
        //    var customers = EntidadCollectionBussinesAction.FindByAll(new EntidadFindParameterEntity(), 0);
        //    outputs.ForEach(x => x.IdClienteAsEntidad = customers.FirstOrDefault(m => m.Id == x.IdCliente));

        //    var outputsDtoList = outputs
        //        .Select(br => br.ToOutputLiteDto())
        //        .ToList();

        //    return outputsDtoList;
        //}

        //public OutputDto GetOutput(string id)
        //{
        //    try
        //    {
        //        var output = GetSalidaEntity(id)?
        //                        .ToOutputDto();

        //        //if (output.CuentasPorCobrar.Count > 0 && output.EstadoActual != "REVISADA")
        //        if (output.CuentasPorCobrar.Count > 0)
        //        {
        //            foreach (var cxc in output.CuentasPorCobrar)
        //            {
        //                cxc.MetodosPago = new List<ReceivablePayMethodDto>();
        //                var metodos = DetalleCobroCollectionBussinesAction.FindByAll(new DetalleCobroFindParameterEntity { IdCuentaPorCobrar = cxc.Id, IdEstado = 1 });
        //                foreach (var meth in metodos)
        //                {
        //                    cxc.MetodosPago.Add(new ReceivablePayMethodDto()
        //                    {
        //                        IdMedioPago = meth.IdMedioPago,
        //                        Valor = meth.Valor,
        //                        MetodoPago = meth.IdMedioPago_DisplayMember,
        //                        Descripcion = meth.Descripcion,
        //                        Id = meth.Id,
        //                        IdCobroDebito = meth.IdCobroDebito,
        //                        IdCierreCaja = meth.IdCierreCaja,
        //                        Estado = true,
        //                        ValorOriginal = meth.Valor
        //                    });
        //                }
        //            }
        //        }

        //        //Cargo el valor de notas de Credito
        //        if (output.EstadoActual == "EN PROCESO")
        //            output.ValorNc = CuentaPorCobrarBussinesAction.ValorNotaCreditoByCliente(output.IdCliente);

        //        if (!string.IsNullOrEmpty(output.TransaccionPadre))
        //        {
        //            output.Entrada = EntradaCollectionBussinesAction
        //                .FindByAll(new EntradaFindParameterEntity { TransaccionPadre = output.TransaccionPadre })?
        //                .FirstOrDefault()?.ToInputDto();
        //        }

        //        return output;
        //    }
        //    catch (HttpResponseException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (SqlException ex)
        //    {
        //        string userError = ex.GetMessage();

        //        if (userError.Contains("PK_Salida"))
        //        {
        //            userError = "No existe la salida especificada";
        //        }
        //        throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
        //            ex.GetAllMessages(), userError));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
        //            ex.GetAllMessages(), ex.GetMessage()));
        //    }
        //}

        //private void SetInfoAdic(SalidaEntity salida, List<AditionalInfoDto> infAdicional)
        //{
        //    if (infAdicional != null)
        //    {
        //        foreach (var inf in infAdicional)
        //        {
        //            if (salida.InfoAdicionalFromIdSalida == null)
        //            {
        //                salida.InfoAdicionalFromIdSalida = new InfoAdicionalEntityCollection();
        //            }

        //            var detalle = new InfoAdicionalEntity
        //            {
        //                TituloInfoAdicional = inf.TituloInfoAdicional.ToUpper(),
        //                InfoAdicional = inf.InfoAdicional.ToUpper()
        //            };

        //            detalle.SetNewState();

        //            salida.InfoAdicionalFromIdSalida.Add(detalle);
        //        }
        //    }

        //}

        //private CobroDebitoEntity CrearRecibo(SalidaEntity salida, string motivo)
        //{
        //    var recibo = new CobroDebitoEntity
        //    {
        //        IdCliente = salida.IdCliente,
        //        Fecha = salida.FechaModificacion ?? salida.FechaIngreso,
        //        Detalle = motivo,
        //        IdEmpresa = salida.IdEmpresa,
        //        IdEstado = 1,
        //        UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso,
        //        IpIngreso = salida.IpModificacion ?? salida.IpIngreso,
        //        FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso
        //    };

        //    recibo.SetNewState();
        //    recibo.DetalleCobroFromIdCobroDebito = new DetalleCobroEntityCollection();
        //    recibo.DetalleMedioCobroFromIdCobroDebito = new DetalleMedioCobroEntityCollection();
        //    return recibo;
        //}

        //private void SetCobroDebito(SalidaEntity salida, List<ReceivableDto> cuentasPorCobrar)
        //{
        //    //Si es cotizacion no generamos cobros
        //    if (salida.TipoTransaccion == "CT")
        //        return;

        //    salida.FechaIngreso = DateTime.Now;

        //    if ((cuentasPorCobrar?.Count ?? 0) == 0)
        //    {
        //        throw new Exception("Debe especificar la información de los pagos de la orden.");
        //    }

        //    foreach (var cxc in cuentasPorCobrar.Where(x => x.ValorPagado > 0))
        //    {
        //        var cxcEntity = salida.CuentaPorCobrarFromIdSalida.FirstOrDefault(m => m.Numero == cxc.Numero);
        //        if (cxcEntity != null)
        //        {
        //            if (salida.ReciboCobro == null)
        //            {
        //                salida.ReciboCobro = CrearRecibo(salida, $"ORDEN # {salida.Id}. {salida.Observaciones}");
        //            }

        //            cxcEntity.DetalleCobroFromIdCuentaPorCobrar = cxcEntity.DetalleCobroFromIdCuentaPorCobrar ?? new DetalleCobroEntityCollection();

        //            //var detalle1 = new DetalleCobroEntity
        //            //{
        //            //    IdCobroDebito = salida.ReciboCobro.Id,
        //            //    IdCobroDebitoAsCobroDebito = salida.ReciboCobro,
        //            //    IdCuentaPorCobrar = cxcEntity.Id,
        //            //    IdCuentaPorCobrarAsCuentaPorCobrar = cxcEntity,
        //            //    IdEstado = 1,
        //            //    IdLocal = salida.IdLocal,

        //            //    Valor = cxc.Valor,

        //            //    FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso,
        //            //    UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso,
        //            //    IpIngreso = salida.IpModificacion ?? salida.IpIngreso
        //            //};

        //            //detalle1.SetNewState();

        //            //cxcEntity.DetalleCobroFromIdCuentaPorCobrar.Add(detalle1);
        //            //salida.ReciboCobro.DetalleCobroFromIdCobroDebito.Add(detalle1);

        //            if (cxc.MetodosPago != null) foreach (var metodo in cxc.MetodosPago)
        //                {

        //                    // Hay que crear un detalle para sustentar el pago.
        //                    var detalle = new DetalleCobroEntity
        //                    {
        //                        IdCobroDebito = salida.ReciboCobro.Id,
        //                        IdCobroDebitoAsCobroDebito = salida.ReciboCobro,
        //                        IdCuentaPorCobrar = cxcEntity.Id,
        //                        IdCuentaPorCobrarAsCuentaPorCobrar = cxcEntity,
        //                        IdEstado = 1,
        //                        IdLocal = salida.IdLocal,

        //                        IdMedioPago = metodo.IdMedioPago,
        //                        Valor = metodo.Valor,
        //                        Descripcion = metodo.Descripcion,

        //                        FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso,
        //                        UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso,
        //                        IpIngreso = salida.IpModificacion ?? salida.IpIngreso
        //                    };

        //                    detalle.SetNewState();

        //                    cxcEntity.DetalleCobroFromIdCuentaPorCobrar.Add(detalle);
        //                    salida.ReciboCobro.DetalleCobroFromIdCobroDebito.Add(detalle);


        //                    // Si se ha creado el detalle sin guardar el medio                            
        //                    var medioCobro = new DetalleMedioCobroEntity
        //                    {
        //                        IdCobroDebito = salida.ReciboCobro.Id,
        //                        IdEstado = 1,
        //                        IdMedioPago = metodo.IdMedioPago,
        //                        Valor = metodo.Valor,
        //                        Descripcion = metodo.Descripcion,

        //                        UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso,
        //                        FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso,
        //                        IpIngreso = salida.IpModificacion ?? salida.IpIngreso,

        //                    };

        //                    medioCobro.SetNewState();
        //                    salida.ReciboCobro.DetalleMedioCobroFromIdCobroDebito.Add(medioCobro);


        //                }
        //            else
        //            {
        //                // Hay que crear un detalle para sustentar el pago.
        //                var detalle = new DetalleCobroEntity
        //                {
        //                    IdCobroDebito = salida.ReciboCobro.Id,
        //                    IdCobroDebitoAsCobroDebito = salida.ReciboCobro,
        //                    IdCuentaPorCobrar = cxcEntity.Id,
        //                    IdCuentaPorCobrarAsCuentaPorCobrar = cxcEntity,
        //                    IdEstado = 1,
        //                    IdLocal = salida.IdLocal,
        //                    IdMedioPago = cxc.IdMedioPago == 0 ? 1 : cxc.IdMedioPago,
        //                    Valor = cxc.ValorPagado,
        //                    FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso,
        //                    UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso,
        //                    IpIngreso = salida.IpModificacion ?? salida.IpIngreso
        //                };

        //                detalle.SetNewState();

        //                cxcEntity.DetalleCobroFromIdCuentaPorCobrar.Add(detalle);
        //                salida.ReciboCobro.DetalleCobroFromIdCobroDebito.Add(detalle);
        //            }


        //        }
        //    }
        //}

        //private SalidaEntity GuardarSalida(SalidaEntity salidaEntity, SqlConnection connection = null, SqlTransaction transaction = null)
        //{

        //    try
        //    {
        //        if (salidaEntity.DetalleSalidaFromIdSalida == null || salidaEntity.DetalleSalidaFromIdSalida.Count == 0)
        //        {
        //            throw new Exception("No se puede guardar un pedido sin detalles, por favor revise su orden.");
        //        }

        //        salidaEntity.IdEmpresa = CurrentUser.IdEmpresa;

        //        if (string.IsNullOrEmpty(salidaEntity.Id))
        //        {
        //            salidaEntity.SetNewState();
        //            salidaEntity.TipoTransaccion = string.IsNullOrEmpty(salidaEntity.TipoTransaccion) ? "NP" : salidaEntity.TipoTransaccion;
        //            salidaEntity.Fecha = DateTime.Now;
        //            salidaEntity.Periodo = salidaEntity.Fecha.ToString("yyyyMM");
        //            salidaEntity.EstadoActual = string.IsNullOrEmpty(salidaEntity.EstadoActual) ? "EN PROCESO" : salidaEntity.EstadoActual;
        //            salidaEntity.FechaIngreso = DateTime.Now;
        //            salidaEntity.IdEstado = 1;
        //            salidaEntity.Total = salidaEntity.DetalleSalidaFromIdSalida.Sum(x => x.Subtotal) + salidaEntity.ValorAdicional;
        //            salidaEntity.Saldo = salidaEntity.Total;

        //            if (salidaEntity.Descuento > 0)
        //            {
        //                var descuento = Math.Round(salidaEntity.Total * salidaEntity.Descuento / 100, 2);
        //                salidaEntity.Total = salidaEntity.Total - descuento;
        //                salidaEntity.Saldo = salidaEntity.Saldo - descuento;
        //            }

        //        }
        //        else
        //        {
        //            salidaEntity.SetUpdatedState();
        //        }

        //        //Configuro el establecimiento y el punto de emision de la factura electronica 
        //        if (salidaEntity.DocumentoFromIdSalida != null && salidaEntity.DocumentoFromIdSalida.Count != 0)
        //        {
        //            foreach (var item in salidaEntity.DocumentoFromIdSalida)
        //            {
        //                item.IdEmpresa = salidaEntity.IdEmpresa;
        //                item.Fecha = item.Fecha.AddHours(-5);

        //                var LocalBodega = LocalBodegaBussinesAction.LoadByPK(salidaEntity.IdLocal);
        //                if (LocalBodega.Establecimiento != null && LocalBodega.PuntoEmision != null)
        //                {
        //                    item.Establecimiento = LocalBodega.Establecimiento;
        //                    item.PuntoEmision = LocalBodega.PuntoEmision;

        //                    //Calculo el descuento
        //                    if (salidaEntity.Descuento > 0)
        //                    {
        //                        //var subtotal = salidaEntity.DetalleSalidaFromIdSalida.Sum(x => x.Subtotal) + salidaEntity.ValorAdicional;
        //                        //item.TotalDescuento = Math.Round((subtotal * salidaEntity.Descuento) / 100,2);                                
        //                        item.TotalDescuento = salidaEntity.Descuento;
        //                    }
        //                }
        //            }
        //        }

        //        // Configuro el metodo que genera las facturas electrónicas
        //        salidaEntity.ProcesarFacturaHandler = OutputExtensions.GenerarFacturas;

        //        var newSalidaEntity = SalidaBussinesAction.Guardar(salidaEntity, connection, transaction);
        //        return newSalidaEntity;
        //    }
        //    catch (HttpResponseException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (SqlException ex)
        //    {
        //        string userError = ex.GetMessage();

        //        if (userError.Contains("PK_Salida"))
        //        {
        //            userError = "No existe la salida especificada";
        //        }
        //        throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
        //           ex.GetAllMessages(), userError));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest,
        //            ex.GetAllMessages(), ex.GetMessage()));
        //    }
        //}

        //internal SalidaEntity GetSalidaEntity(string id)
        //{
        //    var salidaEntity = SalidaBussinesAction.LoadByPK(id, 1);

        //    // Aqui hago dos validaciones, tiene que existir la salida y pertenecer a la empresa actual. 
        //    // De lo contrario no se admite por cuestion de permisos.
        //    if (salidaEntity == null || salidaEntity.IdEmpresa != CurrentUser?.IdEmpresa)
        //        throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, "Salida no existe", "La salida solicitada no existe"));

        //    // Si existe la salida entonces cargo los detalles del documento
        //    salidaEntity.DetalleSalidaFromIdSalida = DetalleSalidaCollectionBussinesAction.FindByAll(new DetalleSalidaFindParameterEntity { IdSalida = salidaEntity.Id, IdEstado = 1 }); ;
        //    salidaEntity.EmpaquetadoFromIdSalida = EmpaquetadoCollectionBussinesAction.FindByAll(new EmpaquetadoFindParameterEntity { IdSalida = salidaEntity.Id, IdEstado = 1 });
        //    salidaEntity.CuentaPorCobrarFromIdSalida = CuentaPorCobrarCollectionBussinesAction.FindByAll(new CuentaPorCobrarFindParameterEntity { IdSalida = salidaEntity.Id, IdEstado = 1 });
        //    salidaEntity.DocumentoFromIdSalida = DocumentoCollectionBussinesAction.FindByAll(new DocumentoFindParameterEntity { IdSalida = salidaEntity.Id, IdEstado = 1 }, 0);


        //    return salidaEntity;
        //}

    }
}
