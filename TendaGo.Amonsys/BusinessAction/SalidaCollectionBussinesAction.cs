
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
    public partial class SalidaCollectionBussinesAction
    {

        //public static SalidaEntityCollection Guardar(SalidaEntityCollection salidaCollection)
        //{
        //    return Guardar(salidaCollection, null, null);
        //}

        //public static SalidaEntityCollection Guardar(SalidaEntityCollection salidaCollection, SqlConnection connection, SqlTransaction transaction)
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

        //        foreach (SalidaEntity salida in salidaCollection)
        //        {
        //            SalidaBussinesAction.Guardar(salida, connection, transaction);
        //        }

        //        if (isBAParent && transaction != null)
        //        {
        //            transaction.Commit();
        //            salidaCollection.SetState(EntityStatesEnum.SavedSuccessfully);
        //        }

        //        return salidaCollection;
        //    }
        //    catch (Exception exc)
        //    {
        //        if (isBAParent && transaction != null)
        //        {
        //            transaction.Rollback();
        //            if (salidaCollection != null) salidaCollection.RollBackState();

        //        }
        //        throw exc;
        //    }
        //    finally
        //    {
        //        if (isBAParent) connection.Close();
        //    }
        //}


        //public static SalidaEntityCollection Search( int idEmpresa, int idLocal, string tipoTransaccion = null, string searchTerm = null, string idVendedor = null, int? idCliente = null, DateTime? startDate = null, DateTime? endDate = null,string estado = null, SqlConnection connection = null, SqlTransaction transaction = null, string transaccionPadre = null)
        //{
        //    bool isBAParent = false;
        //    if (connection == null)
        //    {
        //        isBAParent = true;
        //        connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

        //    }

        //    SalidaEntityCollection salidaCollection = null;

        //    try
        //    {

        //        salidaCollection = SalidaDataAccessCollection.Search( idEmpresa, idLocal, tipoTransaccion, searchTerm, idVendedor, idCliente, startDate, endDate, estado, connection, transaction, transaccionPadre);
        //        salidaCollection.SetState(EntityStatesEnum.Loaded);

        //        return salidaCollection;
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

        //public static DataSet ReporteSalidas(SalidaFindParameterEntity findParameter, SqlConnection connection = null, SqlTransaction transaction = null)
        //{
        //    bool isBAParent = false;
        //    if (connection == null)
        //    {
        //        isBAParent = true;
        //        connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

        //    }

        //    try
        //    {
        //        return SalidaDataAccessCollection.ReporteSalidas(findParameter, connection, transaction);
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
    }
} 

