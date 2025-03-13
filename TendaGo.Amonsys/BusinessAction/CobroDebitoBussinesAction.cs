
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

using System.Data.SqlClient;
using System.Net;
using ER.BE; 
using ER.DA; 
using System.Transactions;
using System.Configuration;

#endregion using

namespace ER.BA
{
    public partial class CobroDebitoBussinesAction
    {
        /// <summary>
        /// Guarda cobro con su detalle 
        /// </summary>
        /// <param name="cobroDebito">el detalle se debe especificar en DetalleCobroFromIdCobroDebito </param>
        /// <returns></returns>
        //public static CobroDebitoEntity Guardar(CobroDebitoEntity cobroDebito)
        //{
        //    return Guardar(cobroDebito, null, null);
        //}
        ///// <summary>
        ///// Guarda cobro con su detalle 
        ///// </summary>
        ///// <param name="cobroDebito">el detalle se debe especificar en DetalleCobroFromIdCobroDebito</param>
        ///// <param name="connection"></param>
        ///// <param name="transaction"></param>
        ///// <returns></returns>
        //public static CobroDebitoEntity Guardar(CobroDebitoEntity cobroDebito, SqlConnection connection, SqlTransaction transaction)
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
        //        cobroDebito = Save(cobroDebito, connection, transaction);

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

    }
}


