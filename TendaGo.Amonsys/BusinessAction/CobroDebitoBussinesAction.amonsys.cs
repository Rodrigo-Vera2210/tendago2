    
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Amonsys Software Factory.
//     Template: Speed Developer FrameWork Version 1.0 (For Windows Applications)
//     Web Site: http://www.amonsys.com/SDF
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



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
using System.Linq;

#endregion using

namespace ER.BA
{
    public partial class CobroDebitoBussinesAction
    {

        //#region Implementation

        //public static CobroDebitoEntity Save(CobroDebitoEntity cobroDebito)
        //{
        //    return Save(cobroDebito, null, null);
        //}

        //public static CobroDebitoEntity Save(CobroDebitoEntity cobroDebito, SqlConnection connection, SqlTransaction transaction)
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
        //        if (cobroDebito.CurrentState != EntityStatesEnum.New)
        //        {
        //            if (cobroDebito.DetalleCobroFromIdCobroDebito != null)
        //            {
        //                // Antes de actualizar o eliminar el registro se debe eliminar los detalles:
        //                var detalleCobroActual = DetalleCobroCollectionBussinesAction.FindByAll(new DetalleCobroFindParameterEntity { IdCobroDebito = cobroDebito.Id }, connection, transaction, 0);

        //                foreach (DetalleCobroEntity detalle in detalleCobroActual)
        //                {
        //                    var cxc = CuentaPorCobrarBussinesAction.LoadByPK(detalle.IdCuentaPorCobrar, connection, transaction);
        //                    cxc.Saldo = cxc.Saldo + detalle.Valor;
        //                    cxc.UsuarioModificacion = cobroDebito.UsuarioModificacion ?? cobroDebito.UsuarioIngreso;
        //                    cxc.IpModificacion = cobroDebito.IpModificacion ?? cobroDebito.IpIngreso;
        //                    cxc.FechaModificacion = cobroDebito.FechaModificacion ?? cobroDebito.FechaIngreso;
        //                    CuentaPorCobrarBussinesAction.Save(cxc, connection, transaction);

        //                    // Ahora marcamos el registro para su eliminacion:
        //                    detalle.SetDeletedState();
        //                }

        //                DetalleCobroCollectionBussinesAction.Save(detalleCobroActual, connection, transaction);
        //            }

        //            if (cobroDebito.DetalleMedioCobroFromIdCobroDebito != null)
        //            {

        //                var detalleMedioCobroActual = DetalleMedioCobroCollectionBussinesAction.FindByAll(new DetalleMedioCobroFindParameterEntity { IdCobroDebito = cobroDebito.Id }, connection, transaction, 0);
        //                foreach (DetalleMedioCobroEntity pago in detalleMedioCobroActual)
        //                {
        //                    pago.SetDeletedState();
        //                }

        //                DetalleMedioCobroCollectionBussinesAction.Save(detalleMedioCobroActual, connection, transaction);
        //            }

                    
        //        }
        //        else
        //        {
        //            // Si se ha creado el detalle sin guardar el medio
        //            if (cobroDebito.DetalleMedioCobroFromIdCobroDebito == null && cobroDebito.DetalleCobroFromIdCobroDebito != null)
        //            {
        //                cobroDebito.DetalleMedioCobroFromIdCobroDebito = new DetalleMedioCobroEntityCollection();
        //                cobroDebito.DetalleMedioCobroFromIdCobroDebito.AddRange(cobroDebito.DetalleCobroFromIdCobroDebito.GroupBy(x => new
        //                {
        //                    IdCobroDebito = x.IdCobroDebito,
        //                    IdEstado = x.IdEstado,
        //                    IdMedioPago = x.IdMedioPago
        //                }).Select(x =>
        //                {
        //                    var medioCobro = new DetalleMedioCobroEntity
        //                    {
        //                        IdCobroDebito = x.Key.IdCobroDebito,
        //                        IdEstado = x.Key.IdEstado,
        //                        IdMedioPago = x.Key.IdMedioPago,

        //                        UsuarioIngreso = x.FirstOrDefault()?.UsuarioIngreso ?? "AUTO",
        //                        FechaIngreso = x.FirstOrDefault()?.FechaIngreso ?? DateTime.Now,
        //                        IpIngreso = x.FirstOrDefault()?.IpIngreso ?? "0.0.0.0",

        //                        UsuarioModificacion = x.FirstOrDefault()?.UsuarioModificacion,
        //                        FechaModificacion = x.FirstOrDefault()?.FechaModificacion,
        //                        IpModificacion = x.FirstOrDefault()?.IpModificacion,

        //                        Valor = x.Sum(m => m.Valor)
        //                    };

        //                    medioCobro.SetNewState();

        //                    return medioCobro;
        //                }));
        //            }
        //        }

        //        //                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
        //        //                {
        //        switch (cobroDebito.CurrentState)
        //        {
        //            case EntityStatesEnum.Deleted:
        //                // Primero se eliminan los detalles luego la cabecera:
        //                CobroDebitoDataAccess.Delete(cobroDebito, connection, transaction);
        //                break;
        //            case EntityStatesEnum.Updated:
        //                // Luego de eliminar los detalles antiguos se actualiza la cabecera:
        //                CobroDebitoDataAccess.Update(cobroDebito, connection, transaction);

        //                if (cobroDebito.DetalleCobroFromIdCobroDebito != null) 
        //                    DetalleCobroCollectionBussinesAction.Save(cobroDebito.DetalleCobroFromIdCobroDebito, connection, transaction);

        //                if (cobroDebito.DetalleMedioCobroFromIdCobroDebito != null) 
        //                    DetalleMedioCobroCollectionBussinesAction.Save(cobroDebito.DetalleMedioCobroFromIdCobroDebito, connection, transaction);
        //                break;
        //            case EntityStatesEnum.New:

        //                //Elimino el cobro debito
        //                //if (cobroDebito.Id!=null && cobroDebito.Id.Length > 0)
        //                //{
        //                //    CobroDebitoDataAccess.Delete(cobroDebito, connection, transaction);
        //                //    cobroDebito.UsuarioModificacion = null;
        //                //    cobroDebito.FechaModificacion = null;
        //                //    cobroDebito.IpModificacion = null;
        //                //}

        //                cobroDebito = CobroDebitoDataAccess.Insert(cobroDebito, connection, transaction);
        //                // Vinculo los detalles con el ID del recibo
        //                if (cobroDebito.DetalleCobroFromIdCobroDebito != null)
        //                {
        //                    cobroDebito.DetalleCobroFromIdCobroDebito?.ForEach(item => {
        //                        item.IdCobroDebito = cobroDebito.Id;
        //                        item.IdEstado = cobroDebito.IdEstado;
        //                        //item.IpIngreso = cobroDebito.IpIngreso;
        //                        //item.FechaIngreso = DateTime.Now;
        //                        //item.UsuarioIngreso = cobroDebito.UsuarioIngreso;
        //                    });
        //                    DetalleCobroCollectionBussinesAction.Save(cobroDebito.DetalleCobroFromIdCobroDebito, connection, transaction);
        //                }

        //                if (cobroDebito.DetalleMedioCobroFromIdCobroDebito!=null)
        //                {
        //                    cobroDebito.DetalleMedioCobroFromIdCobroDebito?.ForEach(item => {
        //                        item.IdCobroDebito = cobroDebito.Id;
        //                        item.IdEstado = cobroDebito.IdEstado;
        //                        //item.IpIngreso = cobroDebito.IpIngreso;
        //                        //item.FechaIngreso = DateTime.Now;
        //                        //item.UsuarioIngreso = cobroDebito.UsuarioIngreso;
        //                    });
        //                    DetalleMedioCobroCollectionBussinesAction.Save(cobroDebito.DetalleMedioCobroFromIdCobroDebito, connection, transaction);
        //                }
        //                break; 
        //        }

        //        //// Ahora empiezo a cambiar los detalles de las cuentas por cobrar afectadas:
        //        //if (cobroDebito.DetalleCobroFromIdCobroDebito != null)
        //        //    foreach (DetalleCobroEntity detalle in cobroDebito.DetalleCobroFromIdCobroDebito)
        //        //    {
        //        //        var cxc = CuentaPorCobrarBussinesAction.LoadByPK(detalle.IdCuentaPorCobrar);
        //        //        // El valor del pago no puede ser mayor al saldo de la cuota
        //        //        if (detalle.Valor > cxc.Saldo)
        //        //        {
        //        //            throw new Exception($"El valor a pagar es superior al saldo de la cuenta por cobrar del Documento #: {cxc.IdSalida} - Cuota #: {cxc.Numero} ");
        //        //        }

        //        //        cxc.Saldo = cxc.Saldo - detalle.Valor;
        //        //        cxc.UsuarioModificacion = cobroDebito.UsuarioModificacion ?? cobroDebito.UsuarioIngreso;
        //        //        cxc.IpModificacion = cobroDebito.IpModificacion ?? cobroDebito.IpIngreso;
        //        //        cxc.FechaModificacion = cobroDebito.FechaModificacion ?? cobroDebito.FechaIngreso;
        //        //        CuentaPorCobrarBussinesAction.Save(cxc, connection, transaction);
        //        //    }

        //        //                } 
        //        //End of Transaction
        //        if (isBAParent && transaction != null)
        //        {
        //            transaction.Commit();
        //            cobroDebito.SetState(EntityStatesEnum.SavedSuccessfully);
        //        }

        //        return cobroDebito;
        //    }
        //    catch (Exception exc)
        //    {
        //        if (isBAParent && transaction != null)
        //        {
        //            transaction.Rollback();
        //            if (cobroDebito != null) cobroDebito.RollBackState();

        //        }
        //        throw exc;
        //    }
        //    finally
        //    {
        //        if (isBAParent) connection.Close();
        //    }
        //}

        //public static CobroDebitoEntity LoadByPK(string Id)
        //{
        //    return LoadByPK(Id, null, null, 1);
        //}

        //public static CobroDebitoEntity LoadByPK(string Id, int deepLoadLevel)
        //{
        //    return LoadByPK(Id, null, null, deepLoadLevel);
        //}

        //public static CobroDebitoEntity LoadByPK(string Id, SqlConnection connection, SqlTransaction transaction)
        //{
        //    return LoadByPK(Id, connection, transaction, 1);
        //}

        //public static CobroDebitoEntity LoadByPK(string Id, SqlConnection connection, SqlTransaction transaction, int deepLoadLevel)
        //{
        //    bool isBAParent = false;
        //    if (connection == null)
        //    {
        //        isBAParent = true;
        //        connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);
        //    }

        //    try
        //    {
        //        CobroDebitoEntity cobroDebito = CobroDebitoDataAccess.LoadByPK(Id, connection, transaction, deepLoadLevel);
        //        if (cobroDebito != null)
        //        {
        //            if (deepLoadLevel > 1)
        //            {

        //            }

        //            cobroDebito.SetLoadedState();
        //        }

        //        return cobroDebito;
        //    }
        //    catch (Exception exc)
        //    {
        //        throw exc;
        //    }
        //    finally
        //    {
        //        if (isBAParent) connection.Close();
        //    }
        //}

        //public static CobroDebitoEntity Reverse(CobroDebitoEntity cobroDebito)
        //{
        //    return Reverse(cobroDebito, null, null);
        //}

        //public static CobroDebitoEntity Reverse(CobroDebitoEntity cobroDebito, SqlConnection connection, SqlTransaction transaction)
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

        //        CobroDebitoDataAccess.Reverse(cobroDebito, connection, transaction);


        //        if (isBAParent && transaction != null)
        //        {
        //            transaction.Commit();
        //            cobroDebito.SetState(EntityStatesEnum.SavedSuccessfully);
        //        }

        //        return cobroDebito;
        //    }
        //    catch (Exception exc)
        //    {
        //        if (isBAParent && transaction != null)
        //        {
        //            transaction.Rollback();
        //            if (cobroDebito != null) cobroDebito.RollBackState();

        //        }
        //        throw exc;
        //    }
        //    finally
        //    {
        //        if (isBAParent) connection.Close();
        //    }
        //}

        //#endregion Implementation
    }
}