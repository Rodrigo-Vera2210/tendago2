using ER.BA;
using ER.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendaGo.Common;

namespace TendaGo.BusinessLogic.Services
{
    internal class IInputService
    {

        //private PagoCreditoEntity CrearReciboI(EntradaEntity entrada, string motivo)
        //{
        //    var recibo = new PagoCreditoEntity
        //    {
        //        IdProveedor = entrada.IdProveedor,
        //        Fecha = entrada.FechaModificacion ?? entrada.FechaIngreso,
        //        Detalle = motivo,
        //        IdEmpresa = entrada.IdEmpresa,
        //        IdEstado = 1,
        //        UsuarioIngreso = entrada.UsuarioModificacion ?? entrada.UsuarioIngreso,
        //        IpIngreso = entrada.IpModificacion ?? entrada.IpIngreso,
        //        FechaIngreso = entrada.FechaModificacion ?? entrada.FechaIngreso
        //    };

        //    recibo.SetNewState();
        //    recibo.DetallePagoFromIdPagoCredito = new DetallePagoEntityCollection();
        //    //recibo.DetalleMedioCobroFromIdCobroDebito = new DetalleMedioCobroEntityCollection();
        //    return recibo;
        //}

        //private void SetPagoCredito(EntradaEntity entrada, List<DebtDto> cuentasPorPagar)
        //{
        //    entrada.FechaIngreso = DateTime.Now;

        //    if ((cuentasPorPagar?.Count ?? 0) == 0)
        //    {
        //        throw new Exception("Debe especificar la información de los pagos de la orden.");
        //    }

        //    foreach (var cxp in cuentasPorPagar.Where(x => x.ValorPagado > 0))
        //    {
        //        var cxpEntity = entrada.CuentaPorPagarFromIdEntrada.FirstOrDefault(m => m.Numero == cxp.Numero);
        //        if (cxpEntity != null)
        //        {
        //            if (entrada.ReciboPago == null)
        //            {
        //                entrada.ReciboPago = CrearReciboI(entrada, $"ORDEN # {entrada.Id}.");
        //            }

        //            cxpEntity.DetallePagoFromIdCuentaPorPagar = cxpEntity.DetallePagoFromIdCuentaPorPagar ?? new DetallePagoEntityCollection();

        //            if (cxp.MetodosPago != null) foreach (var metodo in cxp.MetodosPago)
        //                {

        //                    //// Hay que crear un detalle para sustentar el pago.
        //                    //var detalle = new DetalleCobroEntity
        //                    //{
        //                    //    IdCobroDebito = salida.ReciboCobro.Id,
        //                    //    IdCobroDebitoAsCobroDebito = salida.ReciboCobro,
        //                    //    IdCuentaPorCobrar = cxcEntity.Id,
        //                    //    IdCuentaPorCobrarAsCuentaPorCobrar = cxcEntity,
        //                    //    IdEstado = 1,

        //                    //    IdMedioPago = metodo.IdMedioPago,
        //                    //    Valor = metodo.Valor,
        //                    //    Descripcion = metodo.Descripcion,

        //                    //    FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso,
        //                    //    UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso,
        //                    //    IpIngreso = salida.IpModificacion ?? salida.IpIngreso
        //                    //};

        //                    //detalle.SetNewState();

        //                    //cxcEntity.DetalleCobroFromIdCuentaPorCobrar.Add(detalle);
        //                    //salida.ReciboCobro.DetalleCobroFromIdCobroDebito.Add(detalle);


        //                    //// Si se ha creado el detalle sin guardar el medio                            
        //                    //var medioCobro = new DetalleMedioCobroEntity
        //                    //{
        //                    //    IdCobroDebito = salida.ReciboCobro.Id,
        //                    //    IdEstado = 1,
        //                    //    IdMedioPago = metodo.IdMedioPago,
        //                    //    Valor = metodo.Valor,
        //                    //    Descripcion = metodo.Descripcion,

        //                    //    UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso,
        //                    //    FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso,
        //                    //    IpIngreso = salida.IpModificacion ?? salida.IpIngreso,

        //                    //};

        //                    //medioCobro.SetNewState();
        //                    //salida.ReciboCobro.DetalleMedioCobroFromIdCobroDebito.Add(medioCobro);

        //                }
        //            else
        //            {
        //                // Hay que crear un detalle para sustentar el pago.
        //                var detalle = new DetallePagoEntity
        //                {
        //                    IdPagoCredito = entrada.ReciboPago.Id,
        //                    IdPagoCreditoAsPagoCredito = entrada.ReciboPago,
        //                    IdCuentaPorPagar = cxpEntity.Id,
        //                    IdCuentaPorPagarAsCuentaPorPagar = cxpEntity,
        //                    IdEstado = 1,
        //                    //IdMedioPago = cxc.IdMedioPago == 0 ? 1 : cxc.IdMedioPago,
        //                    Valor = cxp.ValorPagado,
        //                    FechaIngreso = entrada.FechaModificacion ?? entrada.FechaIngreso,
        //                    UsuarioIngreso = entrada.UsuarioModificacion ?? entrada.UsuarioIngreso,
        //                    IpIngreso = entrada.IpModificacion ?? entrada.IpIngreso
        //                };

        //                detalle.SetNewState();

        //                cxpEntity.DetallePagoFromIdCuentaPorPagar.Add(detalle);
        //                entrada.ReciboPago.DetallePagoFromIdPagoCredito.Add(detalle);
        //            }

        //        }
        //    }
        //}


        //private static EntradaEntity GetEntradaEntity(string id)
        //{
        //    var entrada = EntradaBussinesAction.LoadByPK(id);

        //    if (entrada != null)
        //    {
        //        entrada.DetalleEntradaFromIdEntrada = DetalleEntradaCollectionBussinesAction
        //            .FindByAll(new DetalleEntradaFindParameterEntity { IdEntrada = entrada.Id });


        //        entrada.CuentaPorPagarFromIdEntrada = CuentaPorPagarCollectionBussinesAction
        //            .FindByAll(new CuentaPorPagarFindParameterEntity { IdEntrada = entrada.Id });
        //    }

        //    return entrada;
        //}


    }
}
