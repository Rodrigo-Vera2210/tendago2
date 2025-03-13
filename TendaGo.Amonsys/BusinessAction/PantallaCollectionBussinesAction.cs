
//-----------------------------------------------------------------------
// 
//-----------------------------------------------------------------------
// Copyright 2019, Binasystem
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
    public partial class PantallaCollectionBussinesAction
    {
        //public static PantallaEntityCollection ObtenerPantallasPorPerfil(string usuario)
        //{
        //    return ObtenerPantallasPorPerfil(usuario, null, null);
        //}

        //public static PantallaEntityCollection ObtenerPantallasPorPerfil(string usuario, SqlConnection connection, SqlTransaction transaction)
        //{
        //    bool isBAParent = false;
        //    if (connection == null)
        //    {
        //        isBAParent = true;
        //        connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);
        //    }
        //    try
        //    {
        //        return PantallaDataAccessCollection.ObtenerPantallasPorPerfil(usuario, connection, transaction);
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

