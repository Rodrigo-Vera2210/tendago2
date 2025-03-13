
//-----------------------------------------------------------------------
// 
//-----------------------------------------------------------------------
// Copyright 2018, Binasystem
// Autor Edison Romero 
//-----------------------------------------------------------------------



#region using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Net;
using ER.BE;
using ER.DA;
using System.Transactions;
using System.Configuration;

#endregion using

namespace ER.BA
{
    public partial class EntradaBussinesAction
    {
        /// <summary>
        /// Guarda entrada con su detalle, las cuentas por pagar y el pago si existe
        /// </summary>
        /// <param name="entrada">el detalle de entrada y las cuentas por pagar debe especificarse en DetalleEntradaFromIdEntrada y CuentaPorPagarFromIdEntrada</param>
        /// <returns></returns>
        public static EntradaEntity Guardar(EntradaEntity entrada)
        {
            return Guardar(entrada, null, null, null);
        }
        /// <summary>
        /// Guarda entrada con su detalle, las cuentas por pagar y el pago si existe
        /// </summary>
        /// <param name="entrada">el detalle de entrada y las cuentas por pagar debe especificarse en DetalleEntradaFromIdEntrada y CuentaPorPagarFromIdEntrada</param>
        /// <param name="pago">Recibo de pago con su detalle, el detalle debe especificar en DetallePagoFromIdPagoCredito</param>
        /// <returns></returns>
        public static EntradaEntity Guardar(EntradaEntity entrada, PagoCreditoEntity pago)
        {
            return Guardar(entrada, pago, null, null);
        }
        public static EntradaEntity Guardar(EntradaEntity entrada, SqlConnection connection, SqlTransaction transaction)
        {
            return Guardar(entrada, null, connection, transaction);
        }

        /// <summary>
        /// Guarda entrada con su detalle, las cuentas por pagar y el pago si existe
        /// </summary>
        /// <param name="entrada">el detalle de entrada y las cuentas por pagar debe especificarse en DetalleEntradaFromIdEntrada y CuentaPorPagarFromIdEntrada</param>
        /// <param name="pago">Recibo de pago con su detalle, el detalle debe especificar en DetallePagoFromIdPagoCredito</param>
        /// <param name="connection">Conexion</param>
        /// <param name="transaction">Transaccion</param>
        /// <returns></returns>
        public static EntradaEntity Guardar(EntradaEntity entrada, PagoCreditoEntity pago, SqlConnection connection, SqlTransaction transaction)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);
                connection.Open();
                transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            }

            try
            {

                #region Entrada
                entrada = Save(entrada, connection, transaction);
                #endregion

                #region Pago                    
                if (entrada.ReciboPago != null)
                {
                    entrada.ReciboPago = PagoCreditoBussinesAction.Guardar(entrada.ReciboPago, connection, transaction);
                }
                #endregion

                #region Detalle CXP
                if (entrada.CuentaPorPagarFromIdEntrada != null)
                {
                    foreach (CuentaPorPagarEntity cxp in entrada.CuentaPorPagarFromIdEntrada)
                    {
                        cxp.IdEntrada = entrada.Id;
                        cxp.Fecha = entrada.Fecha;
                        cxp.Periodo = entrada.Fecha;
                        cxp.UsuarioIngreso = string.IsNullOrEmpty(entrada.UsuarioModificacion) ? entrada.UsuarioIngreso : entrada.UsuarioModificacion;
                        cxp.IpIngreso = string.IsNullOrEmpty(entrada.IpModificacion) ? entrada.IpIngreso : entrada.IpModificacion;
                        cxp.FechaIngreso = entrada.FechaModificacion ?? entrada.FechaIngreso;
                        cxp.IdEstado = entrada.IdEstado;

                        cxp.SetNewState();
                    }
                    entrada.CuentaPorPagarFromIdEntrada = CuentaPorPagarCollectionBussinesAction.Save(entrada.CuentaPorPagarFromIdEntrada, connection, transaction);
                }
                #endregion

                #region Detalle Pago
                if (entrada.ReciboPago != null && entrada.ReciboPago.DetallePagoFromIdPagoCredito != null  )
                {
                    foreach (DetallePagoEntity item in entrada.ReciboPago.DetallePagoFromIdPagoCredito)
                    {
                        item.IdPagoCredito = entrada.ReciboPago.Id;
                        //Buscar la forma de obtener el ID cuentas or cobrar
                        item.IdCuentaPorPagar = entrada.CuentaPorPagarFromIdEntrada[0].Id;
                        item.UsuarioIngreso = entrada.ReciboPago.UsuarioIngreso;
                        item.IpIngreso = entrada.ReciboPago.IpIngreso;
                        item.FechaIngreso = DateTime.Now;
                        item.IdEstado = entrada.ReciboPago.IdEstado;
                        DetallePagoBussinesAction.Save(item, connection, transaction);
                    }
                }
                #endregion

                #region Salida relacionada solo en caso de anulación
                if (!string.IsNullOrEmpty(entrada.TransaccionPadre) && entrada.CurrentState == EntityStatesEnum.Deleted)
                {
                    var salida = SalidaCollectionBussinesAction
                        .FindByAll(new SalidaFindParameterEntity { TransaccionPadre = entrada.TransaccionPadre }, connection, transaction)?
                        .FirstOrDefault();

                    if (salida != null)
                    {
                        salida.FechaModificacion = entrada.FechaModificacion ?? entrada.FechaIngreso;
                        salida.UsuarioModificacion = entrada.UsuarioModificacion ?? entrada.UsuarioIngreso;
                        salida.IpModificacion = entrada.IpModificacion ?? entrada.IpIngreso;

                        if (entrada.CurrentState == EntityStatesEnum.Deleted)
                        {
                            salida.SetDeletedState();
                            salida.IdEstado = 0;
                        }

                        if (salida.EstadoActual == "ENTREGADA" && entrada.EstadoActual == "ANULADA")
                        {
                            salida.EstadoActual = "ANULADA";
                        }

                        if (entrada.DetalleEntradaFromIdEntrada != null)
                        {
                            salida.DetalleSalidaFromIdSalida = DetalleSalidaCollectionBussinesAction
                                .FindByAll(new DetalleSalidaFindParameterEntity { IdSalida = salida.Id },   connection, transaction,0);

                            if (salida.CurrentState == EntityStatesEnum.Deleted)
                            {
                                salida.DetalleSalidaFromIdSalida.ForEach(x => x.SetDeletedState());
                                //entrada.EstadoActual = "ANULADA";
                                salida.EstadoActual = "ANULADA";
                            }

                            if (salida.DetalleSalidaFromIdSalida != null)
                            {
                                foreach (var detalle in entrada.DetalleEntradaFromIdEntrada)
                                {
                                    var detSalida = salida.DetalleSalidaFromIdSalida.FirstOrDefault(x => x.IdProducto == detalle.IdProducto);
                                    if (detSalida != null)
                                    {
                                        if (detSalida.PreviousState != EntityStatesEnum.Deleted && detSalida.CurrentState != EntityStatesEnum.Deleted)
                                        {
                                            detSalida.IdEstado = detalle.IdEstado;
                                            detSalida.Cantidad = detalle.Cantidad;
                                            detSalida.SetUpdatedState();
                                        }
                                        else
                                        {
                                            // Si ya no existe el detalle en la entrada debe ser eliminado
                                            detSalida.IdEstado = detalle.IdEstado;
                                            detSalida.SetDeletedState();
                                        }
                                    }
                                    else
                                    {
                                        detSalida = new DetalleSalidaEntity
                                        {
                                            IdEmpresa = entrada.IdEmpresa,
                                            Periodo = entrada.Periodo,
                                            Cantidad = detalle.Cantidad,
                                            Fecha = entrada.Fecha,
                                            TipoTransaccion = entrada.TipoTransaccion,
                                            IdSalida = entrada.Id,
                                            IdProducto = detalle.IdProducto,
                                            IdProveedor = entrada.IdProveedor,
                                            IdLocal = entrada.IdLocal,
                                            IdTipoUnidad = detalle.IdTipoUnidad,
                                            FechaFabricacion = detalle.FechaFabricacion,
                                            IdEstado = detalle.IdEstado
                                        };

                                        detSalida.SetNewState();

                                        salida.DetalleSalidaFromIdSalida.Add(detSalida);
                                    }

                                }
                            }
                        }

                        SalidaBussinesAction.Save(salida, connection, transaction);
                    }
                }
                #endregion


                if (isBAParent && transaction != null)
                {
                    transaction.Commit();
                    entrada.SetState(EntityStatesEnum.SavedSuccessfully);
                }

                return entrada;
            }
            catch (Exception exc)
            {
                if (isBAParent && transaction != null)
                {
                    transaction.Rollback();
                    if (entrada != null) entrada.RollBackState();

                }
                throw exc;
            }
            finally
            {
                if (isBAParent) connection.Close();
            }
        }
    }
}


