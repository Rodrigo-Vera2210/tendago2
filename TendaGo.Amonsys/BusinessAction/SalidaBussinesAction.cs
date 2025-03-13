
//-----------------------------------------------------------------------
// 
//-----------------------------------------------------------------------
// Copyright 2018, Binasystem
// Autor Edison Romero 
//-----------------------------------------------------------------------


using System.Linq;
#region using

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using System.Net;
using ER.BE; 
using ER.DA; 
using System.Transactions;
using System.Configuration;

#endregion using

namespace ER.BA
{
    public partial class SalidaBussinesAction
    {     

        /// <summary>
        /// Guarda la salida con su detalle, cuentas por cobrar y recibo 
        /// </summary>
        /// <param name="salida">el detalle de la salida y las cuentas por cobrar deben especificarse en DetalleSalidaFromIdSalida y CuentaPorCobrarFromIdSalida, el recibo de cobro en la porpiedad ReciboCobro</param>
        /// <returns></returns>
        //public static SalidaEntity Guardar(SalidaEntity salida)
        //{
        //    return Guardar(salida, null, null);
        //}

        ///// <summary>
        ///// Guarda la salida con su detalle, cuentas por cobrar y recibo 
        ///// </summary>
        ///// <param name="salida">el detalle de la salida y las cuentas por cobrar deben especificarse en DetalleSalidaFromIdSalida y CuentaPorCobrarFromIdSalida, el recibo de cobro en la porpiedad ReciboCobro </param>
        ///// <param name="connection"></param>
        ///// <param name="transaction"></param>
        ///// <returns></returns>
        //public static SalidaEntity Guardar(SalidaEntity salida, SqlConnection connection, SqlTransaction transaction)
        //{
        //    bool isBAParent = false;
        //    if (connection == null)
        //    {
        //        isBAParent = true;
        //        connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);
        //        connection.Open();
        //        transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

        //    }
            
        //    try
        //    {
        //        #region salida
        //        salida = Save(salida, connection, transaction);
        //        #endregion

                


        //        #region Detalle de Empaquetado
        //        if (salida.EmpaquetadoFromIdSalida != null)
        //        {
        //            foreach (EmpaquetadoEntity item in salida.EmpaquetadoFromIdSalida)
        //            { 
        //                item.IdSalida = salida.Id;
        //                item.IdEstado = salida.IdEstado;
        //                item.IdEmpresa = salida.IdEmpresa;

        //                if (item.CurrentState == EntityStatesEnum.New)
        //                {
        //                    item.IpIngreso = salida.IpIngreso;
        //                    item.UsuarioIngreso = salida.UsuarioIngreso;
        //                    item.FechaIngreso = DateTime.Today;
        //                }
        //                else
        //                {
        //                    item.IpModificacion = salida.IpIngreso;
        //                    item.UsuarioModificacion = salida.UsuarioIngreso;
        //                    item.FechaModificacion = DateTime.Today;
        //                }

        //                // Si la salida se anula los detalles deben actualizarse a 0
        //                if (salida.CurrentState == EntityStatesEnum.Deleted)
        //                {
        //                    item.SetDeletedState();
        //                }

        //                EmpaquetadoBussinesAction.Save(item, connection, transaction);
        //            }
        //        }
        //        #endregion


        //        #region Cuentas por cobrar
        //        if (salida.CuentaPorCobrarFromIdSalida!=null)
        //        {
        //            // Primero Eliminamos, luego insertamos:
        //            foreach (CuentaPorCobrarEntity cxc in salida.CuentaPorCobrarFromIdSalida.Where(m => m.CurrentState == EntityStatesEnum.Deleted))
        //            {
        //                CuentaPorCobrarBussinesAction.Save(cxc, connection, transaction);
        //                // Si hay que eliminar detalles de cobro entonces lo hacemos:
        //                if (cxc.DetalleCobroFromIdCuentaPorCobrar != null)
        //                foreach (var detalleCobro in cxc.DetalleCobroFromIdCuentaPorCobrar.Where(x => x.CurrentState == EntityStatesEnum.Deleted))
        //                {
        //                    DetalleCobroBussinesAction.Save(detalleCobro, connection, transaction);
        //                }
        //            }

        //            foreach (CuentaPorCobrarEntity item in salida.CuentaPorCobrarFromIdSalida.Where(m => m.CurrentState != EntityStatesEnum.Deleted ))
        //            {
        //                item.IdSalida = salida.Id;
        //                item.IdEstado = salida.IdEstado;

        //                if (item.CurrentState == EntityStatesEnum.New)
        //                {
        //                    item.IpIngreso = salida.IpModificacion ?? salida.IpIngreso; ;
        //                    item.UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso;
        //                    item.FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso;
        //                }
        //                else
        //                {
        //                    item.IpModificacion = salida.IpModificacion ?? salida.IpIngreso;
        //                    item.UsuarioModificacion = salida.UsuarioModificacion ?? salida.UsuarioIngreso;
        //                    item.FechaModificacion = salida.FechaModificacion ?? salida.FechaIngreso;
        //                }

        //                CuentaPorCobrarBussinesAction.Save(item, connection, transaction);

        //                // Si hay que eliminar detalles de cobro entonces lo hacemos:
        //                if (item.DetalleCobroFromIdCuentaPorCobrar != null)
        //                {
        //                    foreach(var detalleCobro in item.DetalleCobroFromIdCuentaPorCobrar.Where(x => x.CurrentState == EntityStatesEnum.Updated))
        //                    {
        //                        if ((salida.EstadoActual == "APROBADA" || salida.Entrega == "INMEDIATA") && salida.UsuarioIngreso != detalleCobro.UsuarioIngreso)// actualizo el detalle para registrar que el pago fue hecho por el que aprobó
        //                        {
        //                            DetalleCobroBussinesAction.Save(detalleCobro, connection, transaction);

        //                        }
        //                    }

        //                    foreach (var detalleCobro in item.DetalleCobroFromIdCuentaPorCobrar.Where(x => x.CurrentState == EntityStatesEnum.Deleted))
        //                    {
        //                        DetalleCobroBussinesAction.Save(detalleCobro, connection, transaction);
        //                    }
        //                }
        //            }
        //        }
        //        #endregion

        //        #region Factura
        //        var documentos = new DocumentoEntityCollection();
        //        documentos.AddRange(salida.DocumentoFromIdSalida?.Where(x => x.CurrentState == EntityStatesEnum.New));

        //        if (salida.ProcesarFacturaHandler != null && documentos.Any())
        //        {
        //            salida.DocumentoFromIdSalida = salida.ProcesarFacturaHandler?.Invoke(documentos, connection, transaction);
                    
        //            if (documentos != null)
        //            {
        //                foreach (var item in documentos)
        //                {
        //                    item.IdEmpresa = salida.IdEmpresa;
        //                    item.IdMoneda = 1;
        //                    item.IdSalida = salida.Id;
        //                    item.RUC = salida.Ruc;
        //                    item.IdEstado = salida.IdEstado;

        //                    if (item.CurrentState == EntityStatesEnum.New)
        //                    {
        //                        item.IpIngreso = salida.IpModificacion ?? salida.IpIngreso; ;
        //                        item.UsuarioIngreso = salida.UsuarioModificacion ?? salida.UsuarioIngreso;
        //                        item.FechaIngreso = salida.FechaModificacion ?? salida.FechaIngreso;
        //                    }
        //                    else
        //                    {
        //                        item.IpModificacion = salida.IpModificacion ?? salida.IpIngreso;
        //                        item.UsuarioModificacion = salida.UsuarioModificacion ?? salida.UsuarioIngreso;
        //                        item.FechaModificacion = salida.FechaModificacion ?? salida.FechaIngreso;
        //                    }
        //                }

        //                DocumentoCollectionBussinesAction.Save(salida.DocumentoFromIdSalida, connection, transaction);
        //            }
        //        }
        //        #endregion

        //        #region Recibo de Cobro
        //        if (salida.ReciboCobro!=null)
        //        {
                    
        //            salida.ReciboCobro.IdEmpresa = salida.IdEmpresa;
        //            salida.ReciboCobro.Detalle = $"ORDEN # {salida.Id}. {salida.Observaciones}";

        //            if (salida.ReciboCobro.CurrentState == EntityStatesEnum.New)
        //            {
        //                salida.ReciboCobro.IpIngreso = salida.ReciboCobro.IpModificacion ?? salida.ReciboCobro.IpIngreso; ;
        //                salida.ReciboCobro.UsuarioIngreso = salida.ReciboCobro.UsuarioModificacion ?? salida.ReciboCobro.UsuarioIngreso;
        //                salida.ReciboCobro.FechaIngreso = salida.ReciboCobro.FechaModificacion ?? salida.ReciboCobro.FechaIngreso;
        //            }
        //            else
        //            {
        //                salida.ReciboCobro.IpModificacion = salida.ReciboCobro.IpModificacion ?? salida.ReciboCobro.IpIngreso;
        //                salida.ReciboCobro.UsuarioModificacion = salida.ReciboCobro.UsuarioModificacion ?? salida.ReciboCobro.UsuarioIngreso;
        //                salida.ReciboCobro.FechaModificacion = salida.ReciboCobro.FechaModificacion ?? salida.ReciboCobro.FechaIngreso;
        //            }

        //            salida.ReciboCobro = CobroDebitoBussinesAction.Guardar(salida.ReciboCobro, connection, transaction);
        //        }
        //        #endregion

        //        #region Informacion Adicional
        //        if (salida.InfoAdicionalFromIdSalida != null)
        //        {
        //            foreach (InfoAdicionalEntity item in salida.InfoAdicionalFromIdSalida)
        //            {
        //                item.IdSalida = salida.Id;
        //                InfoAdicionalBussinesAction.Save(item, connection, transaction);                        
        //            }
        //        }
        //        #endregion

        //        #region Entrada relacionada
        //        if (!string.IsNullOrEmpty(salida.TransaccionPadre))
        //        {
        //            var entrada = EntradaCollectionBussinesAction
        //                .FindByAll(new EntradaFindParameterEntity { TransaccionPadre = salida.TransaccionPadre }, connection, transaction)?
        //                .FirstOrDefault();

        //            if (entrada != null)
        //            {
        //                entrada.FechaModificacion = salida.FechaModificacion ?? salida.FechaIngreso;
        //                entrada.UsuarioModificacion = salida.UsuarioModificacion ?? salida.UsuarioIngreso;
        //                entrada.IpModificacion = salida.IpModificacion ?? salida.IpIngreso;

        //                if (salida.CurrentState == EntityStatesEnum.Deleted)
        //                {
        //                    entrada.SetDeletedState();
        //                    entrada.IdEstado = 0;
        //                }

        //                // Una vez entregada la salida por transferencia
        //                // se realiza el envio de la entrada.
        //                if (salida.EstadoActual == "ENTREGADA" && entrada.EstadoActual == "EN PROCESO")
        //                {
        //                    entrada.EstadoActual = "APROBADA";
        //                }

        //                if (salida.DetalleSalidaFromIdSalida != null)
        //                {
        //                    entrada.DetalleEntradaFromIdEntrada = DetalleEntradaCollectionBussinesAction
        //                        .FindByAll(new DetalleEntradaFindParameterEntity { IdEntrada = entrada.Id },   connection, transaction,0);

        //                    if (entrada.CurrentState == EntityStatesEnum.Deleted)
        //                    {
        //                        entrada.DetalleEntradaFromIdEntrada.ForEach(x => x.SetDeletedState());
        //                    }


        //                    if (entrada.DetalleEntradaFromIdEntrada != null)
        //                    {
        //                        foreach (var detalle in salida.DetalleSalidaFromIdSalida)
        //                        {
        //                            var detEntrada = entrada.DetalleEntradaFromIdEntrada.FirstOrDefault(x => x.IdProducto == detalle.IdProducto);
        //                            if (detEntrada != null)
        //                            {
        //                                if (detEntrada.PreviousState != EntityStatesEnum.Deleted && detEntrada.CurrentState != EntityStatesEnum.Deleted)
        //                                {
        //                                    detEntrada.IdEstado = detalle.IdEstado;
        //                                    detEntrada.Cantidad = detalle.Cantidad;
        //                                    detEntrada.SetUpdatedState();
        //                                }
        //                                else
        //                                {
        //                                    // Si ya no existe el detalle en la salida debe ser eliminado
        //                                    detEntrada.IdEstado = detalle.IdEstado;
        //                                    detEntrada.SetDeletedState();
        //                                }
        //                            }
        //                            else
        //                            {
        //                                detEntrada = new DetalleEntradaEntity
        //                                {
        //                                    IdEmpresa = entrada.IdEmpresa,
        //                                    Periodo = entrada.Periodo,
        //                                    Cantidad = detalle.Cantidad,
        //                                    Fecha = entrada.Fecha,
        //                                    TipoTransaccion = entrada.TipoTransaccion,
        //                                    IdEntrada = entrada.Id,
        //                                    IdProducto = detalle.IdProducto,
        //                                    IdProveedor = entrada.IdProveedor,
        //                                    IdLocal = entrada.IdLocal,
        //                                    IdTipoUnidad = detalle.IdTipoUnidad,
        //                                    FechaFabricacion = detalle.FechaFabricacion,
        //                                    IdEstado = detalle.IdEstado
        //                                };

        //                                detEntrada.SetNewState();

        //                                entrada.DetalleEntradaFromIdEntrada.Add(detEntrada);
        //                            }

        //                        }
        //                    }
        //                }

        //                EntradaBussinesAction.Save(entrada, connection, transaction);
        //            }
        //        }
        //        #endregion




        //        if (isBAParent && transaction != null)
        //        {
        //            transaction.Commit();
        //            salida.SetState(EntityStatesEnum.SavedSuccessfully);
        //        }

        //        return salida;
        //    }
        //    catch (Exception exc)
        //    {
        //        if (isBAParent && transaction != null)
        //        {
        //            transaction.Rollback();
        //            if (salida != null) salida.RollBackState();

        //        }
        //        throw exc;
        //    }
        //    finally
        //    {
        //        if (isBAParent) connection.Close();
        //    }
        //}
        //public static SalidaEntity Cambio(SalidaEntity salida, SqlConnection connection, SqlTransaction transaction)
        //{
        //    bool isBAParent = false;
        //    if (connection == null)
        //    {
        //        isBAParent = true;
        //        connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);
        //        connection.Open();
        //        transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

        //    }

        //    try
        //    {
        //        var isNew = string.IsNullOrEmpty(salida.Id);

        //        salida = Change(salida, connection, transaction);
                
                
        //        if (isBAParent && transaction != null)
        //        {
        //            transaction.Commit();
        //            salida.SetState(EntityStatesEnum.SavedSuccessfully);
        //        }

        //        return salida;
        //    }
        //    catch (Exception exc)
        //    {
        //        if (isBAParent && transaction != null)
        //        {
        //            transaction.Rollback();
        //            if (salida != null) salida.RollBackState();

        //        }
        //        throw exc;
        //    }
        //    finally
        //    {
        //        if (isBAParent) connection.Close();
        //    }
        //}
    }
}


